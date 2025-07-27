#pragma warning disable CS1591

namespace Taskify.API.Models
{
    /// <summary>
    /// Join entity that associates a user with a tag in a many-to-many relationship.
    /// </summary>
    public class UserTag
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid TagId { get; set; }
        public Tag Tag { get; set; } = null!;

    }
}
#pragma warning restore  CS1591
