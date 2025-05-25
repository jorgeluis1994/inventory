using Inventory.Application.DTOs;
using Inventory.Domain.Models;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces
{
    /// <summary>
    /// Define las operaciones relacionadas con los usuarios.
    /// </summary>
    public interface IUserService
    {

        Task<LoginResponse?> ValidateLoginAsync(string email, string password);

        Task RegisterUserAsync(UserDto userDto, string password);

        Task<UserDto?> GetByEmailAsync(string email);

        Task<UserDto?> GetUserByIdAsync(Guid id);



    }
}
