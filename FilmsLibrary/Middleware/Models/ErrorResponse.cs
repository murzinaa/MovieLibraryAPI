using System.Net;

namespace FilmsLibrary.Middleware.Models
{
    public class ErrorResponse
    {
        public ErrorResponse(HttpStatusCode statusCode, string message)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; }
    }
} 
