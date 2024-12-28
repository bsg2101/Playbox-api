using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayBox.API.Controllers.Base;
using PlayBox.Application.Common.Models;
using PlayBox.Application.DTOs.Auth;
using PlayBox.Application.Interfaces;

namespace PlayBox.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);
            return HandleResponse(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var response = await _authService.RegisterAsync(registerDto);
            return HandleResponse(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            var response = await _authService.RefreshTokenAsync(refreshTokenDto.RefreshToken);
            return HandleResponse(response);
        }
    }

    public class RefreshTokenDto
    {
        public string RefreshToken { get; set; }
    }
}
