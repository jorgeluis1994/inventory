using Inventory.Domain.Interfaces;
using Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    /// <summary>
    /// Implementación del repositorio para la entidad User.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly InventoryDbContext _dbContext;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="dbContext">Contexto de la base de datos.</param>
        public UserRepository(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u =>
                                        EF.Property<string>(u, "_email") == email);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Users
                .FindAsync(id);
        }

        /// <inheritdoc />
        public async Task<User?> ValidateLoginAsync(string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(passwordHash))
                return null;

             var userExist=await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u =>
                                        EF.Property<string>(u, "_email") == email);


            return userExist;
        }
    }
}
