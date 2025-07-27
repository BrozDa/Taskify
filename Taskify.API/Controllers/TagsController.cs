using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using Taskify.API.Data;
using Taskify.API.Models;
using Taskify.API.Models.Dtos;

namespace Taskify.API.Controllers
{
    /// <summary>
    /// Controller responsible for managing tags
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("/api/[controller]")]
    public class TagsController(TaskifyDbContext context) : Controller
    {
        /// <summary>
        /// Attempts to authenticate user before making any data related operation, by parsing Name identifier claim from JWT token
        /// </summary>
        /// <param name="userId">Returns valid <see cref="Guid"/> userId of authenticated user in case of success, otherwise <see cref="Guid.Empty"/> </param>
        /// <returns>A <c>true</c> in case of successfull authentication</returns>
        private bool AuthenticateUser(out Guid userId)
        {
            return Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
        }
        /// <summary>
        /// Retrieves list of all <see cref="TagDto"/>
        /// </summary>
        /// <returns>
        /// An <see cref="ActionResult{List}"/> containing all <see cref="TagDto"/> associated with user 
        /// </returns>
        [HttpGet("")]
        public async Task<ActionResult<List<TagDto>>> GetTagsAsync()
        {
            if (!AuthenticateUser(out Guid userId)) 
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
        /// <summary>
        /// Adds a new Tag
        /// </summary>
        /// <param name="dto">A AddTagDto representing tag to be added</param>
        /// <returns>
        /// An <see cref="ActionResult{ToDoTaskDto}"/> containing an instance of added <see cref="TagDto"/>
        /// </returns>
        [HttpPost("")]
        public async Task<ActionResult<TagDto>> AddTagAsync([FromBody] AddTagDto dto)
        {

            if (!AuthenticateUser(out Guid userId))
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
        /// <summary>
        /// Adds new tag which is not associated with any task
        /// </summary>
        /// <param name="name">A <see cref="string"/> representing the name of new tag</param>
        /// <returns>
        /// An <see cref="ActionResult{ToDoTaskDto}"/> containing an instance of added <see cref="TagDto"/>
        /// </returns>
        [HttpPost("new/{name}")]
        public async Task<ActionResult<TagDto>> AddTagNewTaskAsync(string name)
        {

            if (!AuthenticateUser(out Guid userId))
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
