namespace Taskify.API.Models.Dtos
{
    /// <summary>
    /// Represents model used while assigning a new tag to existing task
    /// </summary>
    public class AddTagDto
    {
        /// <summary>
        /// Gets or sets task id to which this new tag will be associated to
        /// </summary>
        public Guid TaskId { get; set; }
        /// <summary>
        /// Gets or sets name of the new tag
        /// </summary>
        public string Name { get; set; } = string.Empty;
       
    }
}
