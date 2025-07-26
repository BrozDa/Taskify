using Taskify.API.Data;
using Taskify.API.Models;
using Taskify.API.Models.Dtos;
using Taskify.API.Services.Interfaces;

namespace Taskify.API.Services
{
    public class SeedService(TaskifyDbContext context, IAuthService authService) : ISeedService
    {
        public async Task InsertSeedData()
        {
            if (context.Priorities.Any()) { return; }

            var priorityLow = new Priority() { Name = "Low", Color = "green" };
            var priorityMedium = new Priority() { Name = "Medium", Color = "yellow"};
            var priorityHigh = new Priority() { Name = "High", Color = "orange" };
            var priorityCritical = new Priority() { Name = "Critical", Color = "red" };

            await context.Priorities.AddRangeAsync(priorityLow, priorityMedium, priorityHigh, priorityCritical);
            await context.SaveChangesAsync();

            User adminUser = await authService.RegisterAsync(new UserDto { Username = "admin", Password = "admin", Role = "admin" });
            User standardUser = await authService.RegisterAsync(new UserDto { Username = "user", Password = "user", Role = "user" });

            var tagFamily = new Tag() { Name = "Family", Users = { adminUser, standardUser } };
            var tagWork = new Tag() { Name = "Work", Users = { adminUser, standardUser } };
            var tagStudy = new Tag() { Name = "Study", Users = { adminUser, standardUser } };
            var tagUrgent = new Tag() { Name = "Urgent", Users = { adminUser, standardUser } };
            var tagPersonal = new Tag() { Name = "Personal", Users = { adminUser, standardUser } };
            var tagHealth = new Tag() { Name = "Health", Users = { adminUser, standardUser } };
            var tagShopping = new Tag() { Name = "Shopping", Users = { adminUser, standardUser } };
            var tagFinance = new Tag() { Name = "Finance", Users = { adminUser, standardUser } };
            var tagTravel = new Tag() { Name = "Travel", Users = { adminUser, standardUser } };
            var tagIdeas = new Tag() { Name = "Ideas", Users = { adminUser, standardUser } };
            var tagFitness = new Tag() { Name = "Fitness", Users = { adminUser, standardUser } };
            var tagHome = new Tag() { Name = "Home", Users = { adminUser, standardUser } };
            var adminOnlyTag = new Tag() { Name = "adminOnlyTag", Users = { adminUser } };

            await context.Tags.AddRangeAsync(tagFamily, tagWork, tagStudy, tagUrgent, tagPersonal, tagHealth, tagShopping, tagFinance,
                tagTravel, tagIdeas, tagFitness, tagHome, adminOnlyTag);
            await context.SaveChangesAsync();



            var tasks = new List<ToDoTask>()
            {
                //Admin Tasks
                new ToDoTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Prepare Presentation",
                    Description = "Create slides for Monday's meeting.",
                    DueDate = DateTime.Now.AddDays(2),
                    PriorityId = priorityHigh.Id,
                    Priority = priorityHigh,
                    Tags = new List<Tag> { tagWork, tagUrgent },
                    UserId = adminUser!.Id,
                    User = adminUser!,
                    IsCompleted=true
                },
                new ToDoTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Buy groceries",
                    Description = "Get ingredients for dinner and snacks.",
                    DueDate = DateTime.Now.AddDays(1),
                    PriorityId = priorityMedium.Id,
                    Priority = priorityMedium,
                    Tags = new List<Tag> { tagShopping, tagPersonal },
                    UserId = adminUser!.Id,
                    User = adminUser!,
                    IsCompleted=true
                },
                new ToDoTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Family call",
                    Description = "Weekly video call with parents and siblings.",
                    DueDate = DateTime.Now.AddDays(3),
                    PriorityId = priorityLow.Id,
                    Priority = priorityLow,
                    Tags = new List<Tag> { tagFamily, tagPersonal },
                    UserId = adminUser!.Id,
                    User = adminUser!,
                    IsCompleted=true
                },
                new ToDoTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Workout session",
                    Description = "Attend yoga class in the evening.",
                    DueDate = DateTime.Now.AddDays(1),
                    PriorityId = priorityMedium.Id,
                    Priority = priorityMedium,
                    Tags = new List<Tag> { tagFitness, tagHealth },
                    UserId = adminUser!.Id,
                    User = adminUser!,
                    IsCompleted=false
                },
                new ToDoTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Plan vacation",
                    Description = "Research destinations and book flights.",
                    DueDate = DateTime.Now.AddDays(10),
                    PriorityId = priorityLow.Id,
                    Priority = priorityLow,
                    Tags = new List<Tag> { tagTravel, tagIdeas },
                    UserId = adminUser!.Id,
                    User = adminUser!,
                    IsCompleted=false
                },

                // Potato's Tasks
                new ToDoTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Submit tax documents",
                    Description = "Collect all receipts and fill forms.",
                    DueDate = DateTime.Now.AddDays(5),
                    PriorityId = priorityCritical.Id,
                    Priority = priorityCritical,
                    Tags = new List<Tag> { tagFinance, tagUrgent },
                    UserId = standardUser!.Id,
                    User = standardUser!,
                    IsCompleted=true
                },
                new ToDoTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Finish reading book",
                    Description = "Complete the chapters for next study group.",
                    DueDate = DateTime.Now.AddDays(4),
                    PriorityId = priorityMedium.Id,
                    Priority = priorityMedium,
                    Tags = new List<Tag> { tagStudy, tagIdeas },
                    UserId = standardUser!.Id,
                    User = standardUser!,
                    IsCompleted=true
                },
                new ToDoTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Clean the house",
                    Description = "Vacuum and mop all rooms.",
                    DueDate = DateTime.Now.AddDays(2),
                    PriorityId = priorityHigh.Id,
                    Priority = priorityHigh,
                    Tags = new List<Tag> { tagHome, tagPersonal },
                    UserId = standardUser!.Id,
                    User = standardUser!,
                    IsCompleted=true
                },
                new ToDoTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Prepare work report",
                    Description = "Summarize progress for this quarter.",
                    DueDate = DateTime.Now.AddDays(3),
                    PriorityId = priorityHigh.Id,
                    Priority = priorityHigh,
                    Tags = new List<Tag> { tagWork },
                    UserId = standardUser!.Id,
                    User = standardUser!,
                    IsCompleted=false
                },
                new ToDoTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Doctor appointment",
                    Description = "Annual health check-up.",
                    DueDate = DateTime.Now.AddDays(7),
                    PriorityId = priorityMedium.Id,
                    Priority = priorityMedium,
                    Tags = new List<Tag> { tagHealth },
                    UserId = standardUser!.Id,
                    User = standardUser!,
                    IsCompleted=false
                }
            };

            await context.ToDoTasks.AddRangeAsync(tasks);
            await context.SaveChangesAsync();
        }
    }
}
