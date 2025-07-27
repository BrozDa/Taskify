using Taskify.API.Models.Dtos;

namespace Taskify.API.Models
{
    /// <summary>
    /// Represents a task with a due date, priority, tags, and completion status.
    /// </summary>
    public class ToDoTask
    {
        /// <summary>
        /// Gets or sets the unique identifier for the ToDo task.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the name for the ToDo task.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the description for the ToDo task.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the due date for the ToDo task.
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// Gets or sets the priority Id assigned to this task.
        /// </summary>
        public Guid PriorityId { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="Priority"/> assigned to this task.
        /// </summary>
        public Priority Priority { get; set; } = new();

        /// <summary>
        /// Gets or sets list of <see cref="Tag"/> assigned to this task.
        /// </summary>
        public List<Tag> Tags { get; set; } = new();
        /// <summary>
        /// Gets or sets the user Id assigned to this task.
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="User"/> assigned to this task.
        /// </summary>
        public User User { get; set; } = null!;
        /// <summary>
        /// Gets or Sets value indicating whether task has been already completed
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Converts <see cref="ToDoTask"/> to simpler <see cref="ToDoTaskDto"/> used in API responses
        /// </summary>
        /// <returns>A <see cref="ToDoTask"/> representation in form of <see cref="ToDoTaskDto"/></returns>
        public ToDoTaskDto ToDto()
        {
            return new ToDoTaskDto()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                DueDate = DueDate,
                Priority = Priority.ToDto(),
                Tags = Tags.Select(t => t.ToDto()).ToList()
            };
        }
    }
}
