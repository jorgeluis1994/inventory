using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Application.Interfaces.Security;
using Inventory.Domain.Interfaces;
using Inventory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly ITokenService _tokenService;
        public UserService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public Task<UserDto> GetByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);

            if (user == null)
            {
                return Task.FromResult<UserDto>(result: null );
            }
            var userDto = new UserDto
            {
                UserName = user.Result.UserName,
            };
            return Task.FromResult(userDto);
        }

        public async Task<string> LoginUser(UserDto userDto)
        {
            var userExist = await _userRepository.GetByEmail(userDto.Email);

            if (userExist == null)
            {
                return null;
            }
            var token = _tokenService.GenerateToken(userExist.Id.ToString(), userExist.UserName, userExist.Role, DateTime.UtcNow.AddHours(2));
            return token;
        }

        public Task<bool> RegisterUser(UserDto userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                Password = userDto.Password,
                Role=userDto.Role,
                Email=userDto.Email
            };
            _userRepository.RegisterUser(user);
            return Task.FromResult(true);
        }

    }
}
