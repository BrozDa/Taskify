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
        /// <summary>
        /// Retrieves pending <see cref="ToDoTaskDto"/> for the user
        /// </summary>
        /// <returns>
        /// An <see cref="ActionResult{List}"/> containing all <see cref="ToDoTaskDto"/> pending tasks, or an appropriate error response
        /// </returns>
        [HttpGet("pending")]
        public async Task<ActionResult<List<ToDoTaskDto>>> GetPendingForUser()
        {

            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.GetPendingForUser(userId);

            return result.ToActionResult();
        }
        /// <summary>
        /// Retrieves completed <see cref="ToDoTaskDto"/> for the user
        /// </summary>
        /// <returns>
        /// An <see cref="ActionResult{List}"/> containing all <see cref="ToDoTaskDto"/> completed tasks, or an appropriate error response
        /// </returns>
        [HttpGet("completed")]
        public async Task<ActionResult<List<ToDoTaskDto>>> GetCompletedForUser()
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.GetCompletedForUser(userId);

            return result.ToActionResult();

        }
        /// <summary>
        /// Adds a new task
        /// </summary>
        /// <param name="toDoTaskDto"> A <see cref="ToDoTaskDto"/> containing necessary information for new task</param>
        /// <returns>
        /// An <see cref="ActionResult{ToDoTaskDto}"/> containing the a list of <see cref="ToDoTaskDto"/> of newly added task, or an appropriate error response
        /// </returns>
        [HttpPost("add")]
        public async Task<ActionResult<ToDoTaskDto>> AddTask(ToDoTaskDto toDoTaskDto)
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.AddTask(userId, toDoTaskDto);

            return result.ToActionResult();
        }
        /// <summary>
        /// Deletes a assigned tasks
        /// </summary>>
        /// <param name="taskId">A <see cref="Guid"/> of a task to be deleted</param>
        /// <returns>
        /// An <see cref="ActionResult{ToDoTaskDto}"/> containing the <see cref="ToDoTaskDto"/> of newly added task, or an appropriate error response
        /// </returns>
        [HttpDelete("{taskId}")]
        public async Task<ActionResult<Guid>> DeleteTask(Guid taskId)
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.DeleteTask(userId, taskId);

            return result.ToActionResult();
        }
        /// <summary>
        /// Marks a pending task as completed
        /// </summary>
        /// <param name="taskId">A <see cref="Guid"/> of a task to be completed</param>
        /// <returns>
        /// An <see cref="ActionResult{Guid}"/> containing the <see cref="Guid"/> of completed task, or an appropriate error response
        /// </returns>
        [HttpPatch("{taskId}/complete")]
        public async Task<ActionResult<ToDoTaskDto>> CompleteTask(Guid taskId)
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.CompleteTask(userId, taskId);

            return result.ToActionResult();

        }
        /// <summary>
        /// Update Tags for a pending task 
        /// </summary>
        /// <param name="taskId">A <see cref="Guid"/> of a task for which tags should be updated</param>
        /// <param name="updatedTagIds">A list of <see cref="Guid"/> of tags which should be assigned to the task</param>
        /// <returns>
        /// An <see cref="ActionResult{ToDoTaskDto}"/> containing the updated task on success, or an appropriate error response
        /// </returns>
        [HttpPatch("{taskId}/tags")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateTags(Guid taskId, [FromBody] List<Guid> updatedTagIds)
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.UpdateTags(userId, taskId, updatedTagIds);

            return result.ToActionResult();
        }
        /// <summary>
        /// Updates a priority for pending task
        /// </summary>
        /// <param name="taskId">A <see cref="Guid"/> of a task for which priority should be updated</param>
        /// <param name="dto">A <see cref="PriorityDto"/> containing updated priority</param>
        /// <returns>
        /// An <see cref="ActionResult{ToDoTaskDto}"/> containing the updated task on success, or an appropriate error response
        /// </returns>
        [HttpPatch("{taskId}/priority")]
        public async Task<ActionResult<ToDoTaskDto>> UpdatePriority(Guid taskId, [FromBody] PriorityUpdateDto dto)
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.UpdatePriority(userId, taskId, dto);

            return result.ToActionResult();
        }
        /// <summary>
        /// Updates a name for pending task
        /// </summary>
        /// <param name="taskId">A <see cref="Guid"/> of a task for which name should be updated</param>
        /// <param name="dto">A <see cref="NameUpdateDto"/> containing new name</param>
        /// <returns>
        /// An <see cref="ActionResult{ToDoTaskDto}"/> containing the updated task on success, or an appropriate error response
        /// </returns>
        [HttpPatch("{taskId}/name")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateName(Guid taskId, [FromBody] NameUpdateDto dto)
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.UpdateName(userId, taskId, dto);

            return result.ToActionResult();
        }
        /// <summary>
        /// Updates a description for pending task
        /// </summary>
        /// <param name="taskId">A <see cref="Guid"/> of a task for which description should be updated</param>
        /// <param name="dto">A <see cref="DescriptionUpdateDto"/> containing new description</param>
        /// <returns>
        /// An <see cref="ActionResult{ToDoTaskDto}"/> containing the updated task on success, or an appropriate error response
        /// </returns>
        [HttpPatch("{taskId}/description")]
        public async Task<ActionResult<ToDoTaskDto>> UpdateDescription(Guid taskId, [FromBody] DescriptionUpdateDto dto)
        {
            if (!AuthenticateUser(out Guid userId))
                return Unauthorized();

            var result = await service.UpdateDescription(userId, taskId, dto);

            return result.ToActionResult();
        }
        /// <summary>
        /// Updates a due date for pending task
        /// </summary>
        /// <param name="taskId">A <see cref="Guid"/> of a task for which due date should be updated</param>
        /// <param name="dto">A <see cref="DateUpdateDto"/> containing updated date</param>
        /// <returns>
        /// An <see cref="ActionResult{ToDoTaskDto}"/> containing the updated task on success, or an appropriate error response
        /// </returns>
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
