using System.Text;

namespace FilmsLibrary.Middleware
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestMiddleware> _logger;

        public RequestMiddleware(RequestDelegate next, ILogger<RequestMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            LogRequest(bodyAsText, request);

            context.Request.Body.Seek(0, SeekOrigin.Begin);
            var originalBodyStream = context.Response.Body;


            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                string text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);
   
                _logger.LogInformation($"Execution finished with status code: {context.Response.StatusCode}.{text}");

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private void LogRequest(string requestBody, HttpRequest request)
        {
            if (!string.IsNullOrEmpty(requestBody))
            {
                _logger.LogInformation($"Started executing request.\nEndpoint: {request.Path}.\nRequest: {requestBody}");
            }
            else
            {
                _logger.LogInformation($"Started executing request.\nEndpoint: {request.Path}");
            }
        }
    }
}
