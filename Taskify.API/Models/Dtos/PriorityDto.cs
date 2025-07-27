namespace Taskify.API.Models.Dtos
{
    /// <summary>
    /// Represents a priority of a task.
    /// </summary>
    public class PriorityDto
    {
        /// <summary>
        /// Gets or sets unique identifier for the priority.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets name for the priority.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets description for the priority.
        /// </summary>
        public string Color { get; set; } = string.Empty;
    }
}
