using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskify.API.Data;
using Taskify.API.Models.Dtos;

namespace Taskify.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController(TaskifyDbContext context) : Controller
    {
        [HttpGet("tasks")]
        
        public async Task<List<ToDoTaskDto>> GetAllTasks()
        {
            var result = await context.ToDoTasks
                .Select(t => new ToDoTaskDto()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Priotity = t.Priority.Name,
                    Tags = t.Tags,
                    User= t.User,
                })
                .ToListAsync();

            return result;
        }
    }
}
