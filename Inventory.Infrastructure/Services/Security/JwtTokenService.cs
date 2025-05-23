using Inventory.Application.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Services.Security
{
    public class JwtTokenService : ITokenService
    {
        public string GenerateToken(string userId, string userName, string role, DateTime expirationDate)
        {
            throw new NotImplementedException();
        }
    }
}
