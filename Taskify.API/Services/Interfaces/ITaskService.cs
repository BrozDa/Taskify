using System.Threading.Tasks;
using Taskify.API.Models.Dtos;
using Taskify.API.Services.Shared;

namespace Taskify.API.Services.Interfaces
{
    /// <summary>
    /// Provides operation for managing tasks
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Retrieves pending <see cref="ToDoTaskDto"/> for the user
        /// </summary>
        /// <param name="userId">A <see cref="Guid"/> of user for which the data should be retireved</param>
        /// <returns>A list of <see cref="ToDoTaskDto"/> containing pending tasks</returns>
        Task<TaskServiceResult<List<ToDoTaskDto>>> GetPendingForUser(Guid userId);
        /// <summary>
        /// Retrieves completed <see cref="ToDoTaskDto"/> for the user
        /// </summary>
        /// <param name="userId">A <see cref="Guid"/> of user for which the data should be retireved</param>
        /// <returns>A list of <see cref="ToDoTaskDto"/> containing completed tasks</returns>
        Task<TaskServiceResult<List<ToDoTaskDto>>> GetCompletedForUser(Guid userId);
        /// <summary>
        /// Assigns a new task to to the user and stores it into repository
        /// </summary>
        /// <param name="userId">A <see cref="Guid"/> of user for which task should be assigned</param>
        /// <param name="dto">A <see cref="ToDoTaskDto"/> representation of task to be assigned</param>
        /// <returns>A <see cref="ToDoTaskDto"/> representing newly assigned task</returns>
        Task<TaskServiceResult<ToDoTaskDto>> AddTask(Guid userId, ToDoTaskDto dto);
        /// <summary>
        /// Deletes a assigned tasks from the repository
        /// </summary>
        /// <param name="userId">A <see cref="Guid"/> of user for which task should be deleted</param>
        /// <param name="taskId">A <see cref="Guid"/> of a task to be deleted</param>
        /// <returns>A <see cref="Guid"/> of deleted task</returns>
        Task<TaskServiceResult<Guid>> DeleteTask(Guid userId, Guid taskId);
        /// <summary>
        /// Marks a pending task as completed
        /// </summary>
        /// <param name="userId">A <see cref="Guid"/> of user who have a task assigned</param>
        /// <param name="taskId">A <see cref="Guid"/> of a task to be completed</param>
        /// <returns>A <see cref="Guid"/> of completed task</returns>
        Task<TaskServiceResult<ToDoTaskDto>> CompleteTask(Guid userId, Guid taskId);
        /// <summary>
        /// Update Tags for a pending task 
        /// </summary>
        /// <param name="userId">A <see cref="Guid"/> of user who have a task assigned</param>
        /// <param name="taskId">A <see cref="Guid"/> of a task for which tags should be updated</param>
        /// <param name="updatedTagIds">A list of <see cref="Guid"/> of tags which should be assigned to the task</param>
        /// <returns>A <see cref="ToDoTaskDto"/> with updated tags</returns>
        Task<TaskServiceResult<ToDoTaskDto>> UpdateTags(Guid userId, Guid taskId, List<Guid> updatedTagIds);
        /// <summary>
        /// Updates a priority for pending task
        /// </summary>
        /// <param name="userId">A <see cref="Guid"/> of user who have a task assigned</param>
        /// <param name="taskId">A <see cref="Guid"/> of a task for which priority should be updated</param>
        /// <param name="dto">A <see cref="PriorityDto"/> containing updated priority</param>
        /// <returns>A <see cref="ToDoTaskDto"/> with updated tags</returns>
        Task<TaskServiceResult<ToDoTaskDto>> UpdatePriority(Guid userId, Guid taskId, PriorityUpdateDto dto);
        /// <summary>
        /// Updates a name for pending task
        /// </summary>
        /// <param name="userId">A <see cref="Guid"/> of user who have a task assigned</param>
        /// <param name="taskId">A <see cref="Guid"/> of a task for which name should be updated</param>
        /// <param name="dto">A <see cref="NameUpdateDto"/> containing new name</param>
        /// <returns>A <see cref="ToDoTaskDto"/> with updated name</returns>
        Task<TaskServiceResult<ToDoTaskDto>> UpdateName(Guid userId, Guid taskId, NameUpdateDto dto);
        /// <summary>
        /// Updates a description for pending task
        /// </summary>
        /// <param name="userId">A <see cref="Guid"/> of user who have a task assigned</param>
        /// <param name="taskId">A <see cref="Guid"/> of a task for which description should be updated</param>
        /// <param name="dto">A <see cref="DescriptionUpdateDto"/> containing new description</param>
        /// <returns>A <see cref="ToDoTaskDto"/> with updated description</returns>
        Task<TaskServiceResult<ToDoTaskDto>> UpdateDescription(Guid userId, Guid taskId, DescriptionUpdateDto dto);
        /// <summary>
        /// Updates a due date for pending task
        /// </summary>
        /// <param name="userId">A <see cref="Guid"/> of user who have a task assigned</param>
        /// <param name="taskId">A <see cref="Guid"/> of a task for which due date should be updated</param>
        /// <param name="dto">A <see cref="DateUpdateDto"/> containing updated date</param>
        /// <returns>A <see cref="ToDoTaskDto"/> with updated date</returns>
        Task<TaskServiceResult<ToDoTaskDto>> UpdateDueDate(Guid userId, Guid taskId, DateUpdateDto dto);
    }
}
