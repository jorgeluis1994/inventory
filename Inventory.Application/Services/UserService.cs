using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
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
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<bool> LoginUser(UserDto userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                Password = userDto.Password
            };
            _userRepository.RegisterUser(user);
            return Task.FromResult(true);
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
