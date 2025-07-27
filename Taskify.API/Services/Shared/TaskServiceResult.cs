using System.Net;

namespace Taskify.API.Services.Shared
{
    /// <summary>
    /// Represents model returned by <see cref="TasksService"/> containing all neccessary information for appropriate response
    /// </summary>
    /// <typeparam name="T">A type of data contained in the response</typeparam>
    public class TaskServiceResult<T>
    {
        /// <summary>
        /// Indicates whether the operation was successfull
        /// </summary>
        public bool IsSuccessful { get; set; }
        /// <summary>
        /// Represents status code for HTTP reply
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// Represents error message for unsuccessful requests
        /// </summary>
        public string? ErrorMessage { get; set; }
        /// <summary>
        /// Represents data contained in the reply
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Creates a <see cref="TaskServiceResult{T}"/> representing successful request with the specified data and status code.
        /// </summary>
        /// <param name="Data">Data contained in the reply</param>
        /// <param name="statusCode">Status code representing the result</param>
        /// <returns>A instance of <see cref="TaskServiceResult{T}"/> with the specified data and status code</returns>
        public static TaskServiceResult<T> Success(T Data, HttpStatusCode statusCode) => new() { 
            IsSuccessful = true,
            StatusCode = statusCode,
            Data = Data 
        };
        /// <summary>
        /// Creates a <see cref="TaskServiceResult{T}"/> representing not found request with the specified error message
        /// </summary>
        /// <param name="error">Error message to be contained in the reply</param>
        /// <returns>A instance of <see cref="TaskServiceResult{T}"/> with the specified error message</returns>
        public static TaskServiceResult<T> NotFound(string? error = null) => new() { 
            IsSuccessful = false, 
            ErrorMessage = error,
            StatusCode = HttpStatusCode.NotFound
        };
        /// <summary>
        /// Creates a <see cref="TaskServiceResult{T}"/> representing bad request with the specified error message
        /// </summary>
        /// <param name="error">Error message to be contained in the reply</param>
        /// <returns>A instance of <see cref="TaskServiceResult{T}"/> with the specified error message</returns>
        public static TaskServiceResult<T> BadRequest(string? error = null) => new()
        {
            IsSuccessful = false,
            ErrorMessage = error,
            StatusCode = HttpStatusCode.BadRequest
        };
        /// <summary>
        /// Creates a <see cref="TaskServiceResult{T}"/> representing no content successful request.
        /// </summary>
        /// <returns>>A instance of <see cref="TaskServiceResult{T}"/> representing no content request</returns>
        public static TaskServiceResult<T> NoContent() => new()
        {
            IsSuccessful = true,
            StatusCode = HttpStatusCode.NoContent
        };

    }
}
