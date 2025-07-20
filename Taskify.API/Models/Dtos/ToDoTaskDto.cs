namespace Taskify.API.Models.Dtos
{
    public class ToDoTaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public PriorityDto Priority { get; set; } = new();
        public List<TagDto> Tags { get; set; } = new();

        
    }
}
