namespace Taskify.API.Models.Dtos
{
    public class AddTagDto
    {
        public Guid TaskId { get; set; }
        public string Name { get; set; } = string.Empty;
       
    }
}
