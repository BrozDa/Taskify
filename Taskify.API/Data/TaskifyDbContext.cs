using Microsoft.EntityFrameworkCore;
using Taskify.API.Models;

namespace Taskify.API.Data
{
    public class TaskifyDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Priority> Priorities { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<ToDoTask> ToDoTasks { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {                
            modelBuilder.Entity<ToDoTask>()
                .HasOne(p => p.Priority)
                .WithMany(t => t.Tasks)
                .HasForeignKey(p => p.PriorityId);

            modelBuilder.Entity<ToDoTask>()
                .HasMany(t => t.Tags)
                .WithMany(t => t.Tasks)
                .UsingEntity<TaskTag>(
                  r => r.HasOne<Tag>(tt => tt.Tag).WithMany().HasForeignKey(tt => tt.TagId),
                  l => l.HasOne<ToDoTask>(tt => tt.ToDoTask).WithMany().HasForeignKey(tt => tt.ToDoTaskId)
                );

            modelBuilder.Entity<ToDoTask>()
                .HasOne(u => u.User)
                .WithMany(t => t.Tasks)
                .HasForeignKey(u => u.UserId);

            
        }
    }
    
}
