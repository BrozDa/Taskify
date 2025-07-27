using Microsoft.EntityFrameworkCore;
using Taskify.API.Models;

namespace Taskify.API.Data
{
    /// <summary>
    /// Represents the Entity Framework Core database context for the Taskify application.
    /// </summary>
    /// <param name="options">The options used to configure the context.</param>
    public class TaskifyDbContext(DbContextOptions options) : DbContext(options)
    {
        /// <summary>
        /// Gets or sets the database table for users.
        /// </summary>
        public DbSet<User> Users { get; set; } = null!;

        /// <summary>
        /// Gets or sets the database table for task priorities.
        /// </summary>
        public DbSet<Priority> Priorities { get; set; } = null!;

        /// <summary>
        /// Gets or sets the database table for tags.
        /// </summary>
        public DbSet<Tag> Tags { get; set; } = null!;

        /// <summary>
        /// Gets or sets the database table for tasks.
        /// </summary>
        public DbSet<ToDoTask> ToDoTasks { get; set; } = null!;

        /// <summary>
        /// Configures the entity relationships and constraints using the Fluent API.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for the context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-many: ToDoTask → Priority
            modelBuilder.Entity<ToDoTask>()
                .HasOne(p => p.Priority)
                .WithMany(t => t.Tasks)
                .HasForeignKey(p => p.PriorityId);

            // Many-to-many: ToDoTask ↔ Tag via TaskTag
            modelBuilder.Entity<ToDoTask>()
                .HasMany(t => t.Tags)
                .WithMany(t => t.Tasks)
                .UsingEntity<TaskTag>(
                    r => r.HasOne<Tag>(tt => tt.Tag).WithMany().HasForeignKey(tt => tt.TagId),
                    l => l.HasOne<ToDoTask>(tt => tt.ToDoTask).WithMany().HasForeignKey(tt => tt.ToDoTaskId)
                );

            // Many-to-many: User ↔ Tag via UserTag
            modelBuilder.Entity<User>()
                .HasMany(t => t.Tags)
                .WithMany(t => t.Users)
                .UsingEntity<UserTag>(
                    r => r.HasOne<Tag>(tt => tt.Tag).WithMany().HasForeignKey(tt => tt.TagId),
                    l => l.HasOne<User>(tt => tt.User).WithMany().HasForeignKey(tt => tt.UserId)
                );
        }
    }


}
