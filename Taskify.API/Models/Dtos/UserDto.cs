namespace Taskify.API.Models.Dtos
{
    /// <summary>
    /// Represents a user with associated username, password hash and associated tasks and tags
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets username for the user
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets password for the user
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
