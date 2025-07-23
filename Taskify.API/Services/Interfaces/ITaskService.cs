using System.Threading.Tasks;
using Taskify.API.Models.Dtos;
using Taskify.API.Services.Shared;

namespace Taskify.API.Services.Interfaces
{
    public interface ITaskService
    {
        Task<TaskServiceResult<List<ToDoTaskDto>>> GetPendingForUser(Guid userId);
        Task<TaskServiceResult<List<ToDoTaskDto>>> GetCompletedForUser(Guid userId);
        Task<TaskServiceResult<ToDoTaskDto>> AddTask(Guid userId, ToDoTaskDto dto);
        Task<TaskServiceResult<Guid>> DeleteTask(Guid userId, Guid taskId);
        Task<TaskServiceResult<ToDoTaskDto>> CompleteTask(Guid userId, Guid taskId);
        Task<TaskServiceResult<ToDoTaskDto>> UpdateTags(Guid userId, Guid taskId, List<Guid> updatedTagIds);
        Task<TaskServiceResult<ToDoTaskDto>> UpdatePriority(Guid userId, Guid taskId, PriorityUpdateDto dto);
        Task<TaskServiceResult<ToDoTaskDto>> UpdateName(Guid userId, Guid taskId, NameUpdateDto dto);
        Task<TaskServiceResult<ToDoTaskDto>> UpdateDescription(Guid userId, Guid taskId, DescriptionUpdateDto dto);
        Task<TaskServiceResult<ToDoTaskDto>> UpdateDueDate(Guid userId, Guid taskId, DateUpdateDto dto);
    }
}
