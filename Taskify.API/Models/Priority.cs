using System.Text.Json.Serialization;
using Taskify.API.Models.Dtos;

namespace Taskify.API.Models
{
    /// <summary>
    /// Represents a task priority with an associated name and color.
    /// </summary>
    public class Priority
    {
        /// <summary>
        /// Gets or sets the unique identifier of the priority.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the priority (e.g., High, Medium, Low).
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the color associated with the priority, usually in hex format.
        /// </summary>
        public string Color { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of tasks associated with this priority.
        /// This property is ignored during JSON serialization.
        /// </summary>
        [JsonIgnore]
        public List<ToDoTask>? Tasks { get; set; }

        /// <summary>
        /// Converts this <see cref="Priority"/> instance to a <see cref="PriorityDto"/>.
        /// </summary>
        /// <returns>A <see cref="PriorityDto"/> object containing selected priority details.</returns>
        public PriorityDto ToDto()
        {
            return new PriorityDto { Id = Id, Name = Name, Color = Color };
        }
    }
}
