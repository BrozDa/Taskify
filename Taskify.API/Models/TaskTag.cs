namespace Taskify.API.Models
{
    public class TaskTag
    {
        public Guid ToDoTaskId { get; set; }
        public ToDoTask ToDoTask { get; set; } = null!;
        public Guid TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}
