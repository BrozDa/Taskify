namespace Taskify.API.Models
{

    /// <summary>
    /// Join entity that associates a task with a tag in a many-to-many relationship.
    /// </summary>
    public class TaskTag
    {
        public Guid ToDoTaskId { get; set; }
        public ToDoTask ToDoTask { get; set; } = null!;

        public Guid TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}
