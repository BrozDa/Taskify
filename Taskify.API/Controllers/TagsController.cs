﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskify.API.Data;
using Taskify.API.Models.Dtos;

namespace Taskify.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/[controller]")]
    public class TagsController(TaskifyDbContext context) : Controller
    {
        [HttpGet("all")]
        public async Task<List<TagDto>> GetTagsAsync()
        {
            var result = await context
                .Tags
                .Select(t => new TagDto() { Id=t.Id, Name=t.Name })
                .ToListAsync();

            return result;
        }
    }
}
