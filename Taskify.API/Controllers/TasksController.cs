using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Taskify.API.Models.Dtos;
using Taskify.API.Services.Interfaces;
using Taskify.API.Services.Shared;

namespace Taskify.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController(ITaskService service) : Controller
    {
        private bool AuthorizeUser(out Guid userId)
        {
            return Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        }
        [HttpGet("pending")]
        public async Task<ActionResult<List<ToDoTaskDto>>> GetPendingForUser()
        {

            if (!AuthorizeUser(out Guid userId))
                return Unauthorized();

            var result = await service.GetPendingForUser(userId);

            return result.ToActionResult();
        }
        [HttpGet("completed")]
        public async Task<ActionResult<List<ToDoTaskDto>>> GetCompletedForUser()
        {
            if (!AuthorizeUser(out Guid userId))
                return Unauthorized();

            var result = await service.GetCompletedForUser(userId);

            return result.ToActionResult();

        }
        [HttpPost("add")]
        public async Task<ActionResult<ToDoTaskDto>> AddTask(ToDoTaskDto toDoTaskDto)
        {
            if (!AuthorizeUser(out Guid userId))
                return Unauthorized();

            var result = await service.AddTask(userId, toDoTaskDto);

            return result.ToActionResult();
        }
        [HttpDelete("{taskId}")]
        public async Task<ActionResult<Guid>> DeleteTask(Guid taskId)
        {
            if (!AuthorizeUser(out Guid userId))
                return Unauthorized();

            var result = await service.DeleteTask(userId, taskId);

            return result.ToActionResult();
        }
        [HttpPatch("{taskId}/complete")]
        public async Task<ActionResult<ToDoTaskDto>> CompleteTask(Guid taskId)
        {
            if (!AuthorizeUser(out Guid userId))
                return Unauthorized();

            var result = await service.CompleteTask(userId, taskId);

            return result.ToActionResult();

        }
        [HttpPatch("{taskId}/tags")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateTaskTags(Guid taskId, [FromBody] List<Guid> updatedTagIds)
        {
            if (!AuthorizeUser(out Guid userId))
                return Unauthorized();

            var result = await service.UpdateTags(userId, taskId, updatedTagIds);

            return result.ToActionResult();
        }
        [HttpPatch("{taskId}/priority")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateTaskPriority(Guid taskId, [FromBody] PriorityUpdateDto dto)
        {
            if (!AuthorizeUser(out Guid userId))
                return Unauthorized();

            var result = await service.UpdatePriority(userId, taskId, dto);

            return result.ToActionResult();
        }
        [HttpPatch("{taskId}/name")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateTaskName(Guid taskId, [FromBody] NameUpdateDto dto)
        {
            if (!AuthorizeUser(out Guid userId))
                return Unauthorized();

            var result = await service.UpdateName(userId, taskId, dto);

            return result.ToActionResult();
        }
        [HttpPatch("{taskId}/description")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateTaskDescription(Guid taskId, [FromBody] DescriptionUpdateDto dto)
        {
            if (!AuthorizeUser(out Guid userId))
                return Unauthorized();

            var result = await service.UpdateDescription(userId, taskId, dto);

            return result.ToActionResult();
        }
        [HttpPatch("{taskId}/date")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateTaskDate(Guid taskId, [FromBody] DateUpdateDto dto)
        {
            if (!AuthorizeUser(out Guid userId))
                return Unauthorized();

            var result = await service.UpdateDueDate(userId, taskId, dto);

            return result.ToActionResult();
        }
    }
}
