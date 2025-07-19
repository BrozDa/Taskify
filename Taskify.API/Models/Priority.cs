using Taskify.API.Models.Dtos;

namespace Taskify.API.Models
{
    public class Priority
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BackgroundClass { get; set; } = string.Empty;

        public List<ToDoTask>? Tasks { get; set; }

        public PriorityDto ToDto()
        {
            return new PriorityDto { Id = Id, Name = Name, BackgroundClass= BackgroundClass };
        }
    }
}
