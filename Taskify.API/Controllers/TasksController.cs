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
        [HttpGet("pending")]
        public async Task<ActionResult<List<ToDoTaskDto>>> GetPendingForUser()
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            {
                return Unauthorized();
            }

            var result = await context.ToDoTasks
                .Where(t => t.UserId == userId)
                .Where(t => !t.IsCompleted)
                .Include(t => t.Priority)
                .Include(t=> t.Tags)
                .OrderBy(t => t.DueDate)
                .Select(t => t.ToDto())
                .ToListAsync();

            return Ok(result);
        }
        [HttpGet("completed")]
        public async Task<ActionResult<List<ToDoTaskDto>>> GetCompletedForUser()
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            {
                return Unauthorized();
            }

            var result = await context.ToDoTasks
                .Where(t => t.UserId == userId)
                .Where(t => t.IsCompleted)
                .Include(t => t.Priority)
                .Include(t => t.Tags)
                .OrderBy(t => t.DueDate)
                .Select(t => t.ToDto())
                .ToListAsync();

            return Ok(result);
        }
        [HttpPost("add")]
        public async Task<ActionResult<ToDoTaskDto>> AddTask(ToDoTaskDto toDoTaskDto)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            {
                return Unauthorized();
            }

            var dtoTagIds = toDoTaskDto.Tags.Select(t => t.Id).ToList();
            var existingTags = await context.Tags.Where(t => dtoTagIds.Contains(t.Id)).ToListAsync();
            if (dtoTagIds.Count != existingTags.Count) { return BadRequest("Some of provided tags are invalid"); }

            var existingPriority = await context.Priorities.FindAsync(toDoTaskDto.Priority.Id);
            if(existingPriority is null) { return BadRequest("Provided priority not found"); }

            ToDoTask newTask = new ToDoTask()
            {
                Id= Guid.NewGuid(),
                Name = toDoTaskDto.Name,
                Description = toDoTaskDto.Description,
                DueDate = toDoTaskDto.DueDate,
                PriorityId = existingPriority.Id,
                Priority = existingPriority,
                Tags = existingTags,
                UserId = userId
            };
            await context.ToDoTasks.AddAsync(newTask);
            await context.SaveChangesAsync();

            return Created("",newTask.ToDto());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Guid?>> DeleteTask(Guid id)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            {
                return Unauthorized();
            }

            var task = await context.ToDoTasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null)
                return NotFound();

            context.ToDoTasks.Remove(task);

            await context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPatch("{taskId}/complete")]
        public async Task<ActionResult> CompleteTask(Guid taskId)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            {
                return Unauthorized();
            }

            var task = await context.ToDoTasks.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

            if (task == null)
                return NotFound();

            if (task.IsCompleted)
                return BadRequest("Task is already completed.");

            task.IsCompleted = true;

            await context.SaveChangesAsync();

            return NoContent();

        }
        [HttpPatch("{taskId}/tags")]
        public async Task<ActionResult<List<ToDoTaskDto>>> UpdateTaskTags(Guid taskId, [FromBody] List<Guid> updatedTagIds)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            {
                return Unauthorized();
            }

            var task = await context.ToDoTasks
                .Include(t => t.Tags) 
                .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

            if (task == null)
                return NotFound();

            var existingTags = await context.Tags
                .Where(tag => updatedTagIds.Contains(tag.Id))
                .ToListAsync();

            if (existingTags.Count != updatedTagIds.Count)
                return BadRequest("One or more tag IDs are invalid.");

            task.Tags = existingTags;
            await context.SaveChangesAsync();

            return Ok(task.ToDto());
        }
        [HttpPatch("{taskId}/priority")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateTaskPriority(Guid taskId, [FromBody] PriorityUpdateDto dto)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            {
                return Unauthorized();
            }

            var task = await context.ToDoTasks
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

            if (task == null)
                return NotFound();

            var priority = await context.Priorities.FirstOrDefaultAsync(p => p.Id == dto.UpdatedPriorityId);

            if (priority == null)
                return BadRequest("New priority Id not found");

            task.Priority = priority;

            await context.SaveChangesAsync();

            return Ok(task.ToDto());
        }
        [HttpPatch("{taskId}/name")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateTaskName(Guid taskId, [FromBody] NameUpdateDto dto)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            {
                return Unauthorized();
            }

            var task = await context.ToDoTasks
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

            if (task == null)
                return NotFound();

            task.Name = dto.NewName;
            await context.SaveChangesAsync();

            return Ok(task.ToDto());
        }
        [HttpPatch("{taskId}/description")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateTaskDescription(Guid taskId, [FromBody] DescriptionUpdateDto dto)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            {
                return Unauthorized();
            }

            var task = await context.ToDoTasks
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

            if (task == null)
                return NotFound();

            task.Description = dto.NewDescription;

            await context.SaveChangesAsync();

            return Ok(task.ToDto());
        }
        [HttpPatch("{taskId}/date")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateTaskDate(Guid taskId, [FromBody] DateUpdateDto dto)
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            {
                return Unauthorized();
            }

            var task = await context.ToDoTasks
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

            if (task == null)
                return NotFound();

            task.DueDate = dto.NewDate;

            await context.SaveChangesAsync();

            return Ok(task.ToDto());
        }
    }
}
