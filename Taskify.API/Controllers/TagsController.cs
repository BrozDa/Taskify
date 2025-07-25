﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Taskify.API.Data;
using Taskify.API.Models;
using Taskify.API.Models.Dtos;

namespace Taskify.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]")]
    public class TagsController(TaskifyDbContext context) : Controller
    {
        private bool AuthorizeUser(out Guid userId)
        {
            return Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        }

        [HttpGet("")]
        public async Task<ActionResult<List<TagDto>>> GetTagsAsync()
        {
            if (!AuthorizeUser(out Guid userId)) 
            {
                return Unauthorized();
            }
            var result = await context
                .Tags
                .Where(t=> t.Users.Any(u => u.Id == userId))
                .Select(t => new TagDto() { Id=t.Id, Name=t.Name })
                .OrderBy(t => t.Name)
                .ToListAsync();

            return Ok(result);
        }
        [HttpPost("")]
        public async Task<ActionResult<TagDto>> AddTagAsync([FromBody] AddTagDto dto)
        {

            if (!AuthorizeUser(out Guid userId))
            {
                return Unauthorized();
            }
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Tag name cannot be empty");
            }

            if (await context.Tags
                .Where(t => t.Users.Any(u => u.Id == userId))
                .AnyAsync(t => t.Name.ToLower().Equals(dto.Name.ToLower())))
            {
                return BadRequest("This tag already exists");
            }

            var task = await context.ToDoTasks.FindAsync(dto.TaskId);

            if (task is null)
            {
                return BadRequest("You cannot add tag to non existing task");
            }


            var user = await context.Users.FindAsync(userId);
            var capitalizedName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dto.Name.Trim().ToLower());
            var newTag = new Tag()
            {
                Name = capitalizedName,
                Tasks = new() { task },
                //null forgiving operator is fine - user is already authenticated
                Users = new() { user! },
            };
            await context.Tags.AddAsync(newTag);
            await context.SaveChangesAsync();

            return Ok(newTag.ToDto());
        }
        [HttpPost("new/{name}")]
        public async Task<ActionResult<TagDto>> AddTagNewTaskAsync(string name)
        {

            if (!AuthorizeUser(out Guid userId))
            {
                return Unauthorized();
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Tag name cannot be empty");
            }

            var normalizedName = name.Trim().ToLower();

            var existingTag = await context.Tags
                .Include(t => t.Users)
                .FirstOrDefaultAsync(t => t.Name.ToLower().Equals(normalizedName));

            var user = await context.Users.FindAsync(userId);

            //tag already exists in DB
            if (existingTag is not null) { 
                
                if(existingTag.Users.Any(u=>u.Id == userId)){
                    return BadRequest("This tag already exists");
                }
                else
                {
                    //null forgiving operator is fine - user is already authenticated
                    existingTag.Users.Add(user!);
                    await context.SaveChangesAsync();
                    return Ok(existingTag.ToDto());
                }
            }

            var capitalizedName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(normalizedName);

            var newTag = new Tag()
            {
                Name = capitalizedName,
                Tasks = new(),
                
                Users = new() { user! },
            };
            await context.Tags.AddAsync(newTag);
            await context.SaveChangesAsync();

            return Ok(newTag.ToDto());
        }
    }
}
