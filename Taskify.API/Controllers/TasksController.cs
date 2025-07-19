using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Taskify.API.Data;
using Taskify.API.Models;
using Taskify.API.Models.Dtos;

namespace Taskify.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController(TaskifyDbContext context) : Controller
    {
        [HttpGet("all")]
        public async Task<List<ToDoTaskDto>> GetAllForUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await context.ToDoTasks
                .Where(u => u.UserId.ToString() == userId)
                .Select(t => new ToDoTaskDto()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Priority = t.Priority.ToDto(),
                    Tags = t.Tags.Select(t => t.ToDto()).ToList(),
                })
                .ToListAsync();

            return result;
        }
       
    }
}
