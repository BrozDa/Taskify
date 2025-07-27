using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Taskify.API.Data;
using Taskify.API.Models;
using Taskify.API.Models.Dtos;
using Taskify.API.Services.Interfaces;
using Taskify.API.Services.Shared;

namespace Taskify.API.Services
{
    /// <summary>
    /// Service responsible for managing tasks in the database
    /// Implements <see cref="ITaskService"/>
    /// </summary>
    /// <param name="context">The database context used for accessing user data.</param>
    public class TasksService(TaskifyDbContext context) : ITaskService
    {
        /// <inheritdoc/>
        private async Task<ToDoTask?> GetSingleTaskForUser(Guid userId, Guid taskId)
        {
            return await context
                .ToDoTasks
                .Include(t => t.Tags)
                .Include(t => t.Priority)
                .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);
        }
        /// <inheritdoc/>
        public async Task<TaskServiceResult<List<ToDoTaskDto>>> GetPendingForUser(Guid userId)
        {
            
            var data = await context.ToDoTasks
                .Where(t => t.UserId == userId)
                .Where(t => !t.IsCompleted)
                .Include(t => t.Priority)
                .Include(t => t.Tags)
                .OrderBy(t => t.DueDate)
                .Select(t => t.ToDto())
                .ToListAsync();

            return TaskServiceResult<List<ToDoTaskDto>>.Success(data,HttpStatusCode.OK);
        }
        /// <inheritdoc/>
        public async Task<TaskServiceResult<List<ToDoTaskDto>>> GetCompletedForUser(Guid userId)
        {
            var data = await context.ToDoTasks
                .Where(t => t.UserId == userId)
                .Where(t => t.IsCompleted)
                .Include(t => t.Priority)
                .Include(t => t.Tags)
                .OrderBy(t => t.DueDate)
                .Select(t => t.ToDto())
                .ToListAsync();

            return TaskServiceResult<List<ToDoTaskDto>>.Success(data, HttpStatusCode.OK);
        }
        /// <inheritdoc/>
        public async Task<TaskServiceResult<ToDoTaskDto>> AddTask(Guid userId, ToDoTaskDto dto)
        {
            var dtoTagIds = dto.Tags.Select(t => t.Id).ToList();
            var existingTags = await context.Tags.Where(t => dtoTagIds.Contains(t.Id)).ToListAsync();
            if (dtoTagIds.Count != existingTags.Count)
                return TaskServiceResult<ToDoTaskDto>.BadRequest("One or more of provided tags are invalid");

            var existingPriority = await context.Priorities.FindAsync(dto.Priority.Id);
            if (existingPriority is null)
                return TaskServiceResult<ToDoTaskDto>.BadRequest("Provided priority not found");

            ToDoTask newTask = new ToDoTask()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                DueDate = dto.DueDate,
                PriorityId = existingPriority.Id,
                Priority = existingPriority,
                Tags = existingTags,
                UserId = userId
            };

            await context.ToDoTasks.AddAsync(newTask);
            await context.SaveChangesAsync();

            return TaskServiceResult<ToDoTaskDto>.Success(newTask.ToDto(), HttpStatusCode.Created);
        }
        /// <inheritdoc/>
        public async Task<TaskServiceResult<Guid>> DeleteTask(Guid userId, Guid taskId)
        {
            var task = await GetSingleTaskForUser(userId, taskId);

            if (task is null)
                return TaskServiceResult<Guid>.NotFound();

            context.ToDoTasks.Remove(task);
            await context.SaveChangesAsync();

            return TaskServiceResult<Guid>.NoContent();
        }
        /// <inheritdoc/>
        public async Task<TaskServiceResult<ToDoTaskDto>> CompleteTask(Guid userId, Guid taskId)
        {
            var task = await GetSingleTaskForUser(userId, taskId);

            if (task is null)
                return TaskServiceResult<ToDoTaskDto>.NotFound();

            if (task.IsCompleted)
                return TaskServiceResult<ToDoTaskDto>.BadRequest("Task is already completed");

            task.IsCompleted = true;
            await context.SaveChangesAsync();

            return TaskServiceResult<ToDoTaskDto>.Success(task.ToDto(), HttpStatusCode.OK);    
        }

        /// <inheritdoc/>
        public async Task<TaskServiceResult<ToDoTaskDto>> UpdateTags(Guid userId, Guid taskId, List<Guid> updatedTagIds)
        {
            var task = await GetSingleTaskForUser(userId, taskId);

            if (task is null)
                return TaskServiceResult<ToDoTaskDto>.NotFound();

            var existingTags = await context.Tags
                .Where(tag => updatedTagIds.Contains(tag.Id))
                .ToListAsync();

            if (existingTags.Count != updatedTagIds.Count)
                return TaskServiceResult<ToDoTaskDto>.BadRequest("One or more tag IDs are invalid.");

            task.Tags = existingTags;
            await context.SaveChangesAsync();

            return TaskServiceResult<ToDoTaskDto>.Success(task.ToDto(), HttpStatusCode.OK);
        }
        /// <inheritdoc/>
        public async Task<TaskServiceResult<ToDoTaskDto>> UpdatePriority(Guid userId, Guid taskId, PriorityUpdateDto dto)
        {
            var task = await GetSingleTaskForUser(userId, taskId);

            if (task is null)
                return TaskServiceResult<ToDoTaskDto>.NotFound();

            var priority = await context.Priorities.FirstOrDefaultAsync(p => p.Id == dto.UpdatedPriorityId);

            if (priority == null)
                return TaskServiceResult<ToDoTaskDto>.BadRequest("New priority Id not found");

            task.Priority = priority;

            await context.SaveChangesAsync();

            return TaskServiceResult<ToDoTaskDto>.Success(task.ToDto(), HttpStatusCode.OK);
        }
        /// <inheritdoc/>
        public async Task<TaskServiceResult<ToDoTaskDto>> UpdateName(Guid userId, Guid taskId, NameUpdateDto dto)
        {
            var task = await GetSingleTaskForUser(userId, taskId);

            if (task is null)
                return TaskServiceResult<ToDoTaskDto>.NotFound();

            task.Name = dto.NewName;
            await context.SaveChangesAsync();

            return TaskServiceResult<ToDoTaskDto>.Success(task.ToDto(), HttpStatusCode.OK);
        }
        /// <inheritdoc/>
        public async Task<TaskServiceResult<ToDoTaskDto>> UpdateDescription(Guid userId, Guid taskId, DescriptionUpdateDto dto)
        {
            var task = await GetSingleTaskForUser(userId, taskId);

            if (task is null)
                return TaskServiceResult<ToDoTaskDto>.NotFound();

            task.Description = dto.NewDescription;

            await context.SaveChangesAsync();

            return TaskServiceResult<ToDoTaskDto>.Success(task.ToDto(), HttpStatusCode.OK);
        }
        /// <inheritdoc/>
        public async Task<TaskServiceResult<ToDoTaskDto>> UpdateDueDate(Guid userId, Guid taskId, DateUpdateDto dto)
        {
            var task = await GetSingleTaskForUser(userId, taskId);

            if (task is null)
                return TaskServiceResult<ToDoTaskDto>.NotFound();
            if (dto.NewDate < DateTime.Today)
                return TaskServiceResult<ToDoTaskDto>.BadRequest("Date have to be set for today and onwards");
                

            task.DueDate = dto.NewDate;

            await context.SaveChangesAsync();

            return TaskServiceResult<ToDoTaskDto>.Success(task.ToDto(), HttpStatusCode.OK);
        }
    }
}
