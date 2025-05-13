using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TMA.Application.Middlewares
{
    public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            logger.LogInformation("Handling request: {method} {url}", httpContext.Request.Method, httpContext.Request.Path);

            var request = httpContext.Request;
            var body = string.Empty;
            if (request.Body.CanSeek)
            {
                request.Body.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(request.Body))
                {
                    body = await reader.ReadToEndAsync();
                }
                request.Body.Seek(0, SeekOrigin.Begin);
            }

            logger.LogInformation("Request Body: {body}", body);

            await next(httpContext);
        }
    }
}
