using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Application.DTOs.Auth
{
    public record TokenDto(string Token, DateTime Expiration);
}
