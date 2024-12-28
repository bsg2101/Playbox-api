using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Application.DTOs.Auth
{
    public class TokenDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
