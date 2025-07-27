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
        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="request">A instance of <see cref="UserDto"/> with credentials used for registration</param>
        /// <returns>
        /// An <see cref="ActionResult{List}"/> containing <see cref="User"/> in case of success, null otherwise
        /// </returns>
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
        /// <summary>
        /// Authenticates existing user
        /// </summary>
        /// <param name="request">A instance of <see cref="UserDto"/> with credentials used for authentication</param>
        /// <returns>
        /// An <see cref="ActionResult{String}"/> containing A JWT token in case of success, null otherwise
        /// </returns>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request) 
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
