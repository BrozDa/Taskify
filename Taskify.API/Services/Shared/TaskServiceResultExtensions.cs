using Microsoft.AspNetCore.Mvc;

namespace Taskify.API.Services.Shared
{
    public static class TaskServiceResultExtensions
    {

        public static ActionResult<T> ToActionResult<T>(this TaskServiceResult<T> result)
        {
            if (!result.IsSuccessful)
                return new ObjectResult(result.ErrorMessage) { StatusCode = (int)result.StatusCode };

            if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
                return new NoContentResult();


            return new ObjectResult(result.Data) { StatusCode = (int)result.StatusCode };
        }
    }
}
