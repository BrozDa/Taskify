using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Taskify.API.Data;
using Taskify.API.Models;
using Taskify.API.Models.Dtos;

namespace Taskify.API.Services
{
    public class SeedService(TaskifyDbContext context, IAuthService authService)
    {

        public async Task InsertInitialData()
        {
            try
            {
                await InsertIfSetEmpty(context.Priorities, InsertInitialPriorities);
                await InsertIfSetEmpty(context.Tags, InsertInitialTags);
                await InsertIfSetEmpty(context.Users, InsertInitialUsers);
                await InsertIfSetEmpty(context.ToDoTasks, InsertInitialTasks);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }

        }
        private async Task InsertIfSetEmpty<T>(DbSet<T> set, Func<Task> insertMethod) where T : class
        {
            if(!await set.AnyAsync())
            {
                await insertMethod();
            }
        }

        private async Task InsertInitialPriorities()
        {

            await context.Priorities.AddRangeAsync(
                new List<Priority>()
                {
                    new Priority(){Name="Low"},
                    new Priority(){Name="Medium"},
                    new Priority(){Name="High"},
                    new Priority(){Name="Critical"},

                });

            await context.SaveChangesAsync();
        }
        private async Task InsertInitialTags()
        {
            await context.Tags.AddRangeAsync(
                new List<Tag>()
                {
                    new Tag(){Name="Family"},
                    new Tag(){Name="Work"},
                    new Tag(){Name="Study"},
                });

            await context.SaveChangesAsync();
        }

        private async Task InsertInitialUsers()
        {
            await authService.RegisterAsync(
                new UserDto()
                {
                    Username = "admin",
                    Password = "admin",
                    Role = "admin"
                });
        }

        private async Task InsertInitialTasks()
        {
            var priorities = await context.Priorities.ToListAsync();
            var tags = await context.Tags.ToListAsync();
            var user = await context.Users.ToListAsync();

            var tasks = new List<ToDoTask>()
            {
                new ToDoTask()
                {
                    Name = "Find a job",
                    Description = "It would be nice to find a job",
                    DueDate = DateTime.Now,
                    Priority = priorities.First(p => p.Name == "Critical"),
                    Tags = new List<Tag>()
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
                    Tags = new List<Tag>()
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
                    Tags = new List<Tag>()
                        {

                            tags.First(x => x.Name == "Family")
                        },
                    User = user.First()
                }
            };

            await context.ToDoTasks.AddRangeAsync(tasks);
            await context.SaveChangesAsync();
        }
    }
}
