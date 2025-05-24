
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces.Security
{
    
    public interface ITokenService
    {
        string GenerateToken(string userId, string userName, string role, DateTime expirationDate);

    }
}
