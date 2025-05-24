using Inventory.Domain.Interfaces;
using Inventory.Domain.Models;
using Inventory.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
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
        private readonly PasswordHasher<User> _passwordHasher;
        public UserRepository(InventoryDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }
        public Task<bool> LoginUser(User userDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterUser(User userDto)
        {
            //userDto.Password = _passwordHasher.HashPassword(userDto, userDto.Password);
            await _context.Users.AddAsync(userDto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
