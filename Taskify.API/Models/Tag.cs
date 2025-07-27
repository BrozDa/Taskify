using System.Text.Json.Serialization;
using Taskify.API.Models.Dtos;

namespace Taskify.API.Models
{
    /// <summary>
    /// Represents a task tag which can be assigned to specific tasks or users.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Gets or sets the unique identifier for the tag.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets name of the tag.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets task which have this tag assigned
        /// </summary>
        [JsonIgnore]
        public List<ToDoTask> Tasks { get; set; } = new();

        /// <summary>
        /// Gets or sets users who can use this tag
        /// </summary>
        public List<User> Users { get; set; } = new();

        /// <summary>
        /// Converts this <see cref="Tag"/> to <see cref="TagDto"/>
        /// </summary>
        /// <returns>A <see cref="TagDto"/> containing the tag's ID and name.</returns>
        public TagDto ToDto()
        {
            return new TagDto { Id = Id, Name = Name };
        }
    }
}
