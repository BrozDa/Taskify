using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskify.API.Data;
using Taskify.API.Models.Dtos;

namespace Taskify.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrioritiesController(TaskifyDbContext context) : Controller
    {
        [HttpGet("")]
        public async Task<ActionResult<List<PriorityDto>>> GetPrioritiesAsync()
        {
            var result = await context
                .Priorities
                .Select(p => new PriorityDto() { Id=p.Id, Name=p.Name, Color=p.Color})
                .ToListAsync();

            return Ok(result);
        }
    }
}
