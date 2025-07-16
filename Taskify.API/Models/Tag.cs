namespace Taskify.API.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<TaskTag> Tags { get; set; } = new();
    }
}
