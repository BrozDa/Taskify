using Taskify.API.Models;
using Taskify.API.Models.Dtos;

namespace Taskify.API.Services.Interfaces
{
    /// <summary>
    /// Provides authentication-related operations -  registration and login.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="request">A instance of <see cref="UserDto"/> with credentials used for registration</param>
        /// <returns>A instance of <see cref="User"/> in case of success, null otherwise</returns>
        Task<User?> RegisterAsync(UserDto request);
        /// <summary>
        /// Authenticates existing user
        /// </summary>
        /// <param name="request">A instance of <see cref="UserDto"/> with credentials used for authentication</param>
        /// <returns>A JWT token in case of success, null otherwise</returns>
        Task<string?> LoginAsync(UserDto request);
    }
}
