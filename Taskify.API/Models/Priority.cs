namespace Taskify.API.Models
{
    public class Priority
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ToDoTask>? Tasks { get; set; }
    }
}
