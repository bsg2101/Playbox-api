using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PlayBox.Application.Common.Models;
using PlayBox.Application.DTOs.Auth;
using PlayBox.Application.Interfaces;
using PlayBox.Domain.Entities;
using PlayBox.Domain.Interfaces;
using PlayBox.Domain.Enums;

namespace PlayBox.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(
            IUnitOfWork unitOfWork,
            IConfiguration configuration,
            IOptions<JwtSettings> jwtSettings,
            IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _jwtSettings = jwtSettings.Value;
            _passwordHasher = passwordHasher;
        }

        public async Task<ServiceResponse<TokenDto>> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var user = await _unitOfWork.Users.GetByEmailAsync(loginDto.Email);

                if (user == null)
                    return new ServiceResponse<TokenDto>(false, "Geçersiz email veya şifre");

                var verificationResult = _passwordHasher.VerifyHashedPassword(
                    user, user.PasswordHash, loginDto.Password);

                if (verificationResult == PasswordVerificationResult.Failed)
                    return new ServiceResponse<TokenDto>(false, "Geçersiz email veya şifre");

                var token = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await _unitOfWork.Users.UpdateAsync(user);
                await _unitOfWork.CompleteAsync();

                var tokenDto = new TokenDto
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    ExpiresIn = _jwtSettings.DurationInMinutes * 60,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = user.Role.ToString()
                };

                return new ServiceResponse<TokenDto>(true, "Login başarılı", tokenDto);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<TokenDto>(false, $"Login işlemi sırasında bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<TokenDto>> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                var existingUser = await _unitOfWork.Users.GetByEmailAsync(registerDto.Email);

                if (existingUser != null)
                    return new ServiceResponse<TokenDto>(false, "Bu email adresi zaten kayıtlı");

                var user = new User
                {
                    Email = registerDto.Email,
                    UserName = registerDto.UserName,
                    CreatedAt = DateTime.UtcNow,
                    Role = UserRole.User
                };

                user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);

                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                var token = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await _unitOfWork.Users.UpdateAsync(user);
                await _unitOfWork.CompleteAsync();

                var tokenDto = new TokenDto
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    ExpiresIn = _jwtSettings.DurationInMinutes * 60,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = user.Role.ToString()
                };

                return new ServiceResponse<TokenDto>(true, "Kayıt başarılı", tokenDto);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<TokenDto>(false, $"Kayıt işlemi sırasında bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<TokenDto>> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                // Burada tüm kullanıcıları çekmek zorundayız çünkü refresh token'a göre arama yapıyoruz
                var users = await _unitOfWork.Users.GetAllAsync();
                var user = users.FirstOrDefault(u => u.RefreshToken == refreshToken);

                if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                    return new ServiceResponse<TokenDto>(false, "Geçersiz veya süresi dolmuş refresh token");

                var newToken = GenerateJwtToken(user);
                var newRefreshToken = GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await _unitOfWork.Users.UpdateAsync(user);
                await _unitOfWork.CompleteAsync();

                var tokenDto = new TokenDto
                {
                    Token = newToken,
                    RefreshToken = newRefreshToken,
                    ExpiresIn = _jwtSettings.DurationInMinutes * 60,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = user.Role.ToString()
                };

                return new ServiceResponse<TokenDto>(true, "Token yenilendi", tokenDto);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<TokenDto>(false, $"Token yenileme sırasında bir hata oluştu: {ex.Message}");
            }
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}