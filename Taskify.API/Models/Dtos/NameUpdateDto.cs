namespace Taskify.API.Models.Dtos
{
    /// <summary>
    /// Represents model used for updating a task name
    /// </summary>
    public class NameUpdateDto
    {
        /// <summary>
        /// Gets or sets a new task name
        /// </summary>
        public string NewName { get; set; } = string.Empty;
    }
}
