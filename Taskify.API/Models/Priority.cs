﻿using System.Text.Json.Serialization;
using Taskify.API.Models.Dtos;

namespace Taskify.API.Models
{
    public class Priority
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

        [JsonIgnore]
        public List<ToDoTask>? Tasks { get; set; }

        public PriorityDto ToDto()
        {
            return new PriorityDto { Id = Id, Name = Name, Color = Color };
        }
    }
}
