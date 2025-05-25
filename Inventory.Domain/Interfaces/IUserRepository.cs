using Inventory.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    /// <summary>
    /// Repositorio para la entidad User.
    /// </summary>
    public interface IUserRepository
    {
        Task<User?> ValidateLoginAsync(string email, string passwordHash);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task AddAsync(User user);
    }
}
