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
        Task<bool> LoginUser(UserDto userDto);
   
    }
}
