using Inventory.Domain.Interfaces;
using Inventory.Domain.Models;
using Inventory.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly InventoryDbContext _context;
        public UserRepository(InventoryDbContext context)
        {
            _context = context;
        }
        public Task<bool> LoginUser(User userDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterUser(User userDto)
        {
            await _context.Users.AddAsync(userDto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
