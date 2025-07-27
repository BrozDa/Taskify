namespace Taskify.API.Models.Dtos
{
    /// <summary>
    /// Represents model used for updating a task description
    /// </summary>
    public class DescriptionUpdateDto
    {
        /// <summary>
        /// Gets or sets a new task description
        /// </summary>
        public string NewDescription { get; set; } = string.Empty;
    }
}
