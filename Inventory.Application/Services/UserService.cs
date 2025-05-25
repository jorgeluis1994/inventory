using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Application.Security;
using Inventory.Domain.Interfaces;
using Inventory.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para gestionar usuarios.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authService;

        public UserService(IUserRepository userRepository, IAuthenticationService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        /// <inheritdoc />
        public async Task<LoginResponse?> ValidateLoginAsync(string email, string password)
        {
            var user = await _userRepository.ValidateLoginAsync(email, password);
            if (user == null) return null;

            bool validPassword = _authService.VerifyPassword(user.Password, password);
            if (!validPassword) return null;

            var expirationDate = DateTime.UtcNow.AddHours(1);
            var token = _authService.GenerateToken(user.Id.ToString(), user.UserName, expirationDate);

            return new LoginResponse
            {
                Email = user.Email,
                Token = token
            };
        }


        /// <inheritdoc />
        public async Task RegisterUserAsync(UserDto userDto, string password)
        {
            var existingUser = await _userRepository.GetByEmailAsync(userDto.Email);
            if (existingUser != null)
                throw new InvalidOperationException("El correo ya está registrado.");

            var hashedPassword = _authService.HashPassword(password);

            var user = new User(userDto.UserName, hashedPassword, userDto.Email);

            await _userRepository.AddAsync(user);
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return null;

            return UserDto.FromDomain(user);
        }

        public Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

     
    }
}
