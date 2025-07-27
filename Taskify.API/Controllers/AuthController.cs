using Microsoft.AspNetCore.Mvc;
using Taskify.API.Models;
using Taskify.API.Models.Dtos;
using Taskify.API.Services.Interfaces;

namespace Taskify.API.Controllers
{
    /// <summary>
    /// Controller responsible for handling user authentication and registration.
    /// </summary>
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        /// <inheritdoc/>
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
        /// <inheritdoc/>
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
