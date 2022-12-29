using Newtonsoft.Json;
using System.Net;
using FilmsLibrary.Middleware.Models;
using FilmsLibrary.Models.Domain.Exceptions;

namespace FilmsLibrary
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var message = exception.Message;
            var exceptionType = exception.GetType();
            context.Response.ContentType = "application/json";

            var statusCode = GetStatusCode(exceptionType);
            context.Response.StatusCode = (int)statusCode;
            var errorDetails = new ErrorResponse(statusCode, message);
            var logLevel = GetLogLevel(statusCode);

            LogException(logLevel, message, exception);
            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorDetails));
        }

        private HttpStatusCode GetStatusCode(Type exceptionType)
        {
            var statusCodes = new Dictionary<Type, HttpStatusCode>
            {
                [typeof(NotFoundException)] = HttpStatusCode.NotFound,
            };

            return statusCodes.TryGetValue(exceptionType, out HttpStatusCode statusCode) ? statusCode : HttpStatusCode.InternalServerError;
        }

        private LogLevel GetLogLevel(HttpStatusCode statusCode)
        {
            var logLevels = new Dictionary<HttpStatusCode, LogLevel>
            {
                [HttpStatusCode.NotFound] = LogLevel.Warning,
                [HttpStatusCode.InternalServerError] = LogLevel.Error,
            };

            return logLevels.TryGetValue(statusCode, out LogLevel logLevel) ? logLevel : LogLevel.Error;
        }

        private void LogException(LogLevel logLevel, string message, Exception exception)
        {
            switch (logLevel)
            {
                case LogLevel.Warning:
                    _logger.LogWarning(message);
                    break;
                case LogLevel.Error:
                    _logger.LogError(exception, message);
                    break;
                default:
                    _logger.Log(logLevel, exception, message);
                    break;
            }
        }
    }
}
