namespace Taskify.API.Models
{
    public class ToDoTask
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }

        public Guid PriorityId { get; set; }
        public Priority Priority { get; set; } = new();

        public List<Tag> Tags { get; set; } = new();

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

    }
}
