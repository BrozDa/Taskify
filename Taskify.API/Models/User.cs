﻿namespace Taskify.API.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public List<ToDoTask>? Tasks { get; set; }

        public List<Tag>? Tags { get; set; } = new();

    }
}
