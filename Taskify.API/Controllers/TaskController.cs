using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskify.API.Data;
using Taskify.API.Models;

namespace Taskify.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class TaskController(TaskifyDbContext context) : Controller
    {
        [HttpGet("tasks")]
        [Authorize]
        public async Task<List<ToDoTask>> GetAllTasks()
        {
            var result = await context.ToDoTasks
                .Include(p => p.Priority)
                .ToListAsync();

            return result;
        }
        /*public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }

        public Guid PriorityId { get; set; }
        public Priority Priority { get; set; } = new();

        public List<Tag> Tags { get; set; } = new();

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;*/
    }
}
