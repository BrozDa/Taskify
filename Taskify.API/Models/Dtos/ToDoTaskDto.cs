namespace Taskify.API.Models.Dtos
{
    /// <summary>
    /// Represents a task with a due date, priority, tags, and completion status.
    /// </summary>
    public class ToDoTaskDto
    {
        /// <summary>
        /// Gets or sets unique identifier for the ToDo task.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets name for the ToDo task.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets descriptio for the ToDo task.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets DueDate for the ToDo task.
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// Gets or sets a priority for the ToDo task.
        /// </summary>
        public PriorityDto Priority { get; set; } = new();
        /// <summary>
        /// Gets or sets a listof tags for the ToDo task.
        /// </summary>
        public List<TagDto> Tags { get; set; } = new();

        
    }
}
