using Microsoft.AspNetCore.Mvc;
using Taskify.API.Models;
using Taskify.API.Models.Dtos;
using Taskify.API.Services.Interfaces;

namespace Taskify.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await authService.RegisterAsync(request);
            if (user is null)
            {
                return BadRequest("User already exists");
            }
            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserDto request) 
        {
            var token = await authService.LoginAsync(request);

            if (token is null)
            {
                return BadRequest("Invalid username or password");
            }
            return Ok(token);
        }

        
    }
}
