using AutoMapper;
using PlayBox.Application.Common.Models;
using PlayBox.Application.DTOs.Auth;
using PlayBox.Application.Interfaces;
using PlayBox.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        // JWT configuration eklenecek

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TokenDto>> LoginAsync(LoginDto loginDto)
        {
            // Login implementasyonu
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<TokenDto>> RegisterAsync(RegisterDto registerDto)
        {
            // Register implementasyonu
            throw new NotImplementedException();
        }
    }
}
