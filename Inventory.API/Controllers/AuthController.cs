using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        // POST api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return BadRequest("Email and password must be provided.");
            }

            var user = await _userService.ValidateLoginAsync(loginRequest.Email, loginRequest.Password);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(user);
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterRequest registerRequest)
        {
            if (registerRequest == null
                || string.IsNullOrWhiteSpace(registerRequest.UserName)
                || string.IsNullOrWhiteSpace(registerRequest.Email)
                || string.IsNullOrWhiteSpace(registerRequest.Password))
            {
                return BadRequest("Username, email, and password must be provided.");
            }

            var userExists = await _userService.GetByEmailAsync(registerRequest.Email);
            if (userExists != null)
            {
                return Conflict("A user with this email already exists.");
            }

            var userDto = new UserDto
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.Email
                // Pon aquí otros campos si los tienes en UserDto
            };

            await _userService.RegisterUserAsync(userDto, registerRequest.Password);

            // Puedes devolver algo adecuado, por ejemplo:
            return CreatedAtAction(nameof(Register), new { email = userDto.Email }, userDto);
        }

    }
}
