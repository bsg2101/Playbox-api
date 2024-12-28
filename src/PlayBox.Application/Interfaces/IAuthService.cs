using PlayBox.Application.Common.Models;
using PlayBox.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse<TokenDto>> LoginAsync(LoginDto loginDto);
        Task<ServiceResponse<TokenDto>> RegisterAsync(RegisterDto registerDto);
        Task<ServiceResponse<TokenDto>> RefreshTokenAsync(string refreshToken);
    }
}
