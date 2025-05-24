using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> RegisterUser(User userDto);

        Task<User> GetByEmail(string email);
    }
}
