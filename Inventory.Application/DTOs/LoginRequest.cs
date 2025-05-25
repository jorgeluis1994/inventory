using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.DTOs
{
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string? Password { get; set; } = null!;
    }

    public class LoginResponse
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; }
    }

}
