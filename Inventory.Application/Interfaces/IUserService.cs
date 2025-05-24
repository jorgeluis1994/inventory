using Inventory.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserDto userDto);
        Task<string> LoginUser(UserDto userDto);
        Task<UserDto> GetByEmail(string email);


    }
}
