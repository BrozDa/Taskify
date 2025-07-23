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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await context.ToDoTasks
                .Where(u => u.UserId.ToString() == userId)
                .Where(c => !c.IsCompleted)
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
        [HttpGet("completed")]
        public async Task<ActionResult<List<ToDoTaskDto>>> GetCompletedForUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await context.ToDoTasks
                .Where(u => u.UserId.ToString() == userId)
                .Where(c => c.IsCompleted)
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
        [HttpPatch("{taskId}/complete")]
        public async Task<ActionResult> CompleteTask(Guid taskId)
        {
            var task = await context.ToDoTasks.FirstOrDefaultAsync(t => t.Id == taskId);
            if(task is null) { return  NotFound(); }

            task.IsCompleted = true;

            await context.SaveChangesAsync();

            return Ok();

        }
        [HttpPatch("{taskId}/tags")]
        public async Task<ActionResult<List<ToDoTaskDto>>> UpdateTaskTags(Guid taskId, [FromBody] List<Guid> updatedTagIds)
        {
            var task = await context.ToDoTasks
                .Include(t => t.Tags) 
                .FirstOrDefaultAsync(t => t.Id == taskId);

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
            var task = await context.ToDoTasks
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == taskId);

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
            var task = await context.ToDoTasks
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
                return NotFound();

            task.Name = dto.NewName;
            await context.SaveChangesAsync();

            return Ok(task.ToDto());
        }
        [HttpPatch("{taskId}/description")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateTaskDescription(Guid taskId, [FromBody] DescriptionUpdateDto dto)
        {
            var task = await context.ToDoTasks
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
                return NotFound();

            task.Description = dto.NewDescription;

            await context.SaveChangesAsync();

            return Ok(task.ToDto());
        }
        [HttpPatch("{taskId}/date")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateTaskDate(Guid taskId, [FromBody] DateUpdateDto dto)
        {
            var task = await context.ToDoTasks
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            if (task == null)
                return NotFound();

            task.DueDate = dto.NewDate;

            await context.SaveChangesAsync();

            return Ok(task.ToDto());
        }
    }
}
