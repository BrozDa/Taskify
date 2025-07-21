using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
        public async Task<ActionResult<List<ToDoTaskDto>>> GetAllForUser()
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
                .OrderBy(p => p.DueDate)
                .ToListAsync();

            return Ok(result);
        }
        [HttpPost("add")]
        public async Task<ActionResult<ToDoTaskDto>> AddTask(ToDoTaskDto toDoTaskDto)
        {
            
            var dtoTagIds = toDoTaskDto.Tags.Select(t => t.Id).ToList();
            var existingTags = await context.Tags.Where(t => dtoTagIds.Contains(t.Id)).ToListAsync();
            var existingPriority = await context.Priorities.FindAsync(toDoTaskDto.Priority.Id);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ToDoTask newTask = new ToDoTask()
            {
                Id= Guid.NewGuid(),
                Name = toDoTaskDto.Name,
                Description = toDoTaskDto.Description,
                DueDate = toDoTaskDto.DueDate,
                PriorityId = existingPriority!.Id,
                Priority = existingPriority,
                Tags = existingTags,
                UserId = Guid.Parse(userId!)
            };
            await context.ToDoTasks.AddAsync(newTask);
            await context.SaveChangesAsync();

            return Created("",newTask.ToDto());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Guid?>> DeleteTask(Guid id)
        {
            if (!await context.ToDoTasks.AnyAsync(t => t.Id == id))
            {
                return NotFound();
            }
            await context.ToDoTasks.Where(x => x.Id == id).ExecuteDeleteAsync();

            return Ok(id);
        }
    }
}
