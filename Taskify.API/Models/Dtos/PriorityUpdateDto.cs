namespace Taskify.API.Models.Dtos
{
    /// <summary>
    /// Represents model used for updating a task priority
    /// </summary>
    public class PriorityUpdateDto
    {
        /// <summary>
        /// Gets or sets a new task priority
        /// </summary>
        public Guid UpdatedPriorityId { get; set; }
    }
}
