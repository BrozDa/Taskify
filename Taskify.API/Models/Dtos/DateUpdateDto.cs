namespace Taskify.API.Models.Dtos
{
    /// <summary>
    /// Represents model used while updating a time
    /// </summary>
    public class DateUpdateDto
    {
        /// <summary>
        /// Gets or sets a new date for a task
        /// </summary>
        public DateTime NewDate { get; set; }
    }
}
