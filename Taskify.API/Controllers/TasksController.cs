using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Taskify.API.Models.Dtos;
using Taskify.API.Services.Interfaces;
using Taskify.API.Services.Shared;

namespace Taskify.API.Controllers
{

    /// <summary>
    /// Controller responsible for managing tasks
    /// </summary>
    /// <param name="service"></param>
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController(ITaskService service) : Controller
    {
        /// <summary>
        /// Attempts to authenticate user before making any data related operation, by parsing Name identifier claim from JWT token
        /// </summary>
        /// <param name="userId">Returns valid <see cref="Guid"/> userId of authenticated user in case of success, otherwise <see cref="Guid.Empty"/> </param>
        /// <returns>A <c>true</c> in case of successfull authentication</returns>
        private bool AuthenticateUser(out Guid userId)
        {
            return Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        }
        /// <inheritdoc/>
        [HttpGet("pending")]
        public async Task<ActionResult<List<ToDoTaskDto>>> GetPendingForUser()
        {

            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.GetPendingForUser(userId);

            return result.ToActionResult();
        }
        /// <inheritdoc/>
        [HttpGet("completed")]
        public async Task<ActionResult<List<ToDoTaskDto>>> GetCompletedForUser()
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.GetCompletedForUser(userId);

            return result.ToActionResult();

        }
        /// <inheritdoc/>
        [HttpPost("add")]
        public async Task<ActionResult<ToDoTaskDto>> AddTask(ToDoTaskDto toDoTaskDto)
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.AddTask(userId, toDoTaskDto);

            return result.ToActionResult();
        }
        /// <inheritdoc/>
        [HttpDelete("{taskId}")]
        public async Task<ActionResult<Guid>> DeleteTask(Guid taskId)
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.DeleteTask(userId, taskId);

            return result.ToActionResult();
        }
        /// <inheritdoc/>
        [HttpPatch("{taskId}/complete")]
        public async Task<ActionResult<ToDoTaskDto>> CompleteTask(Guid taskId)
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.CompleteTask(userId, taskId);

            return result.ToActionResult();

        }
        /// <inheritdoc/>
        [HttpPatch("{taskId}/tags")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateTags(Guid taskId, [FromBody] List<Guid> updatedTagIds)
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.UpdateTags(userId, taskId, updatedTagIds);

            return result.ToActionResult();
        }
        /// <inheritdoc/>
        [HttpPatch("{taskId}/priority")]
        public async Task<ActionResult<ToDoTaskDto>> UpdatePriority(Guid taskId, [FromBody] PriorityUpdateDto dto)
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.UpdatePriority(userId, taskId, dto);

            return result.ToActionResult();
        }
        /// <inheritdoc/>
        [HttpPatch("{taskId}/name")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateName(Guid taskId, [FromBody] NameUpdateDto dto)
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.UpdateName(userId, taskId, dto);

            return result.ToActionResult();
        }
        /// <inheritdoc/>
        [HttpPatch("{taskId}/description")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateDescription(Guid taskId, [FromBody] DescriptionUpdateDto dto)
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.UpdateDescription(userId, taskId, dto);

            return result.ToActionResult();
        }
        /// <inheritdoc/>
        [HttpPatch("{taskId}/due-date")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateDueDate(Guid taskId, [FromBody] DateUpdateDto dto)
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.UpdateDueDate(userId, taskId, dto);

            return result.ToActionResult();
        }
    }
}
