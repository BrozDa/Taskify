using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Taskify.API.Data;
using Taskify.API.Models;
using Taskify.API.Models.Dtos;

namespace Taskify.API.Services
{
    public class SeedService(TaskifyDbContext context, IAuthService authService)
    {
        public async Task InsertSeedData()
        {
            if(context.Priorities.Any()) { return; }

            var priorityLow = new Priority() { Name = "Low", BackgroundClass = "green" };
            var priorityMedium = new Priority() { Name = "Medium", BackgroundClass = "yellow" };
            var priorityHigh = new Priority() { Name = "High", BackgroundClass = "orange" };
            var priorityCritical = new Priority() { Name = "Critical", BackgroundClass = "red" };

            await context.Priorities.AddRangeAsync(priorityLow, priorityMedium, priorityHigh, priorityCritical);
            await context.SaveChangesAsync();

            var tagFamily = new Tag() { Name = "Family" };
            var tagWork = new Tag() { Name = "Work" };
            var tagStudy = new Tag() { Name = "Study" };
            var tagUrgent = new Tag() { Name = "Urgent" };
            var tagPersonal = new Tag() { Name = "Personal" };
            var tagHealth = new Tag() { Name = "Health" };
            var tagShopping = new Tag() { Name = "Shopping" };
            var tagFinance = new Tag() { Name = "Finance" };
            var tagTravel = new Tag() { Name = "Travel" };
            var tagIdeas = new Tag() { Name = "Ideas" };
            var tagFitness = new Tag() { Name = "Fitness" };
            var tagHome = new Tag() { Name = "Home" };

            await context.Tags.AddRangeAsync(tagFamily, tagWork, tagStudy, tagUrgent,tagPersonal,tagHealth,tagShopping,tagFinance,
                tagTravel, tagIdeas,tagFitness,tagHome);
            await context.SaveChangesAsync();

            var adminUser = await authService.RegisterAsync(new UserDto { Username="admin", Password="admin", Role="admin"});
            var standardUser = await authService.RegisterAsync(new UserDto { Username = "user", Password = "user", Role = "user"});


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
        /*public async Task InsertInitialData()
        {
            try
            {
                var priorities = await InsertInitialPriorities();
                var tags = await InsertInitialTags();
                var users = await InsertInitialUsers();

                await InsertInitialTasks(priorities, tags, users);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private async Task<List<Priority>?> InsertInitialPriorities()
        {
            if(await context.Priorities.AnyAsync()) { return null; }

            var items = new List<Priority>()
            {
                new Priority() { Name = "Low", BackgroundClass = "bg-green-200 text-green-800" },
                new Priority() { Name = "Medium", BackgroundClass = "bg-yellow-200 text-yellow-800" },
                new Priority() { Name = "High", BackgroundClass = "bg-orange-300 text-orange-900" },
                new Priority() { Name = "Critical", BackgroundClass = "bg-red-500 text-white" },
            };

            await context.Priorities.AddRangeAsync(items);

            await context.SaveChangesAsync();

            return items;

        }
        private async Task<List<Tag>?> InsertInitialTags()
        {
            if (await context.Tags.AnyAsync()) { return null; }

            var items = new List<Tag>()
            {
                    new Tag(){ Name = "Family" },
                    new Tag(){ Name = "Work" },
                    new Tag(){ Name = "Study" },
                    new Tag(){ Name = "Urgent" },
                    new Tag(){ Name = "Personal" },
                    new Tag(){ Name = "Health" },
                    new Tag(){ Name = "Shopping" },
                    new Tag(){ Name = "Finance" },
                    new Tag(){ Name = "Travel" },
                    new Tag(){ Name = "Ideas" },
                    new Tag(){ Name = "Fitness" },
                    new Tag(){ Name = "Home" }
            };

            await context.Tags.AddRangeAsync(items);
            await context.SaveChangesAsync();

            return items;
        }
        private async Task<List<User>?> InsertInitialUsers()
        {
            if(await context.Users.AnyAsync()) { return null;}
            var items = new List<UserDto>()
            {
                new UserDto()
                {
                    Username = "daniel",
                    Password = "daniel",
                    Role = "admin"
                },
                new UserDto()
                {
                    Username = "potato",
                    Password = "milacek",
                    Role = "admin"
                }
            };
            var users = new List<User>();
            foreach(var item in items)
            {
                var result = await authService.RegisterAsync(item);

                if(result is not null) { users.Add(result); }
            }
            return users;
        }

        private async Task InsertInitialTasks(List<Priority>? priorities, List<Tag>? tags, List<User>? users)
        {
            if(priorities is null || tags is null || users is null) return;


            /*var tasks = new List<ToDoTask>()
            {
                new ToDoTask()
                {
                    Name = "Prepare Presentation",
                    Description = "Create slides for Monday's meeting.",
                    DueDate = DateTime.Now.AddDays(2),
                    PriorityId = priorities.Find(p => p.Name == "Critical").Id,
                    Priority = priorityHigh,
                    Tags = new List<Tag> { tagWork, tagUrgent },
                    UserId = adminUser!.Id,
                    User = adminUser!
                },

                new ToDoTask()
                {
                    Name = "Find a job",
                    Description = "It would be nice to find a job",
                    DueDate = DateTime.Now,
                    Priority = priorities.First(p => p.Name == "Critical"),
                    Tags = new List<Models.Tag>()
                        {

                            tags.First(x => x.Name == "Work"),
                            tags.First(x => x.Name == "Study"),
                        },
                    User = user.First()
                },
                new ToDoTask()
                {
                    Name = "Finish Taskify project",
                    Description = "Learned so much already, working on more",
                    DueDate = DateTime.Now,
                    Priority = priorities.First(p => p.Name == "High"),
                    Tags = new List<Models.Tag>()
                        {

                            tags.First(x => x.Name == "Study")
                        },
                    User = user.First()
                },
                new ToDoTask()
                {
                    Name = "Visit family",
                    Description = "Havent seen them for quite some time",
                    DueDate = DateTime.Now,
                    Priority = priorities.First(p => p.Name == "Medium"),
                    Tags = new List<Models.Tag>()
                        {

                            tags.First(x => x.Name == "Family")
                        },
                    User = user.First()
                }
            };

            await context.ToDoTasks.AddRangeAsync(tasks);
            await context.SaveChangesAsync();
        }*/
    }
}
