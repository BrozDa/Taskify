namespace Taskify.API.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ToDoTask> Tasks { get; set; } = new();
    }
}
