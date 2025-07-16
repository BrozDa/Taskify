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
            var result = await context.ToDoTasks.ToListAsync();
            return result;
        }
    }
}
