using Inventory.Domain.Models;
using System;

namespace Inventory.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Convierte una entidad User a UserDto.
        /// </summary>
        public static UserDto FromDomain(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        /// <summary>
        /// Convierte un UserDto a entidad User, requiere la contraseña hasheada.
        /// </summary>
        /// <param name="passwordHash">Contraseña hasheada.</param>
        public User ToDomain(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password hash is required.", nameof(passwordHash));

            return new User(UserName, passwordHash, Email);
        }
    }
}
