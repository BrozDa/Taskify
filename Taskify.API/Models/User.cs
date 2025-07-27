namespace Taskify.API.Models
{
    /// <summary>
    /// Represents a user with associated username, password hash and associated tasks and tags
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets username for the user
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the password hash generated while registering
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets list of <see cref="ToDoTask"/> associated with this user
        /// </summary>

        public List<ToDoTask>? Tasks { get; set; }

        /// <summary>
        /// Gets or sets list of <see cref="Tag"/> associated with this user
        /// </summary>

        public List<Tag>? Tags { get; set; } = new();

    }
}
