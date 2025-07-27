using Microsoft.AspNetCore.Mvc;

namespace Taskify.API.Services.Shared
{
    /// <summary>
    /// Provides extension method for converting <see cref="TaskServiceResult{T}"/> instances
    /// into <see cref="ActionResult{T}"/> responses.
    /// </summary>
    public static class TaskServiceResultExtensions
    {
        /// <summary>
        /// Converts <see cref="TaskServiceResult{T}"/> into <see cref="ActionResult{T}"/>
        /// </summary>
        /// <typeparam name="T">A type of data contained in the response</typeparam>
        /// <param name="result">A <see cref="TaskServiceResult{T}"/> returned by the service</param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> representing either:
        /// <list type="bullet">
        /// <item>A successful result with the data and appropriate HTTP status code.</item>
        /// <item>An error result with the error message and appropriate HTTP status code.</item>
        /// <item>A <see cref="NoContentResult"/> if the status code indicates no content.</item>
        /// </list>
        /// </returns>
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
