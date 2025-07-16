namespace Taskify.API.Models.Dtos
{
    public class ToDoTaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string Priotity { get; set; } = string.Empty;
        public List<Tag> Tags { get; set; } = new();
        public User User { get; set; }
    }
}
