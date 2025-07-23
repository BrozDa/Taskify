using System.Net;

namespace Taskify.API.Services.Shared
{
    public class TaskServiceResult<T>
    {
        public bool IsSuccessful { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }

        public static TaskServiceResult<T> Success(T Data, HttpStatusCode statusCode) => new() { 
            IsSuccessful = true,
            StatusCode = statusCode,
            Data = Data 
        };
        public static TaskServiceResult<T> NotFound(string? error = null) => new() { 
            IsSuccessful = false, 
            ErrorMessage = error,
            StatusCode = HttpStatusCode.NotFound
        };
        public static TaskServiceResult<T> BadRequest(string? error = null) => new()
        {
            IsSuccessful = false,
            ErrorMessage = error,
            StatusCode = HttpStatusCode.BadRequest
        };
        public static TaskServiceResult<T> NoContent() => new()
        {
            IsSuccessful = true,
            StatusCode = HttpStatusCode.NoContent
        };

    }
}
