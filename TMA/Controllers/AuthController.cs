using Microsoft.AspNetCore.Mvc;
using TMA.Application.Authentication;
using TMA.Application.DTOs.Authentication;
using TMA.Domain.Interfaces.Services;

namespace TMA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IJwtAuthenticationService jwtAuthenticationService, IUsersService usersService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {

            if (userLoginDto == null)
            {
                return Unauthorized();
            }

            // 1. Check if user exists
            var user = await usersService.GetByEmailAndPassword(userLoginDto.Email, userLoginDto.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            // 2. Generate JWT token
            var token = jwtAuthenticationService.GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userRegisterDto)
        {
            if (userRegisterDto == null)
            {
                return BadRequest();
            }
            // 1. Check if user with the same email exists
            var existingUser = await usersService.GetByEmail(userRegisterDto.Email);
            if (existingUser != null)
            {
                return BadRequest("User with the same email already exists");
            }
            // 2. Add user
            await usersService.Add(userRegisterDto);
            return Ok();
        }
    }
}
