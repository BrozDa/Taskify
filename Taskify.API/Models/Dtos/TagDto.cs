namespace Taskify.API.Models.Dtos
{
    /// <summary>
    /// Represents a priority of a task.
    /// </summary>
    public class TagDto
    {
        /// <summary>
        /// Gets or sets unique identifier for the tag.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets name for the tag.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
