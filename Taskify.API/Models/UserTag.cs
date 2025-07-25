namespace Taskify.API.Models
{
    public class UserTag
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}
