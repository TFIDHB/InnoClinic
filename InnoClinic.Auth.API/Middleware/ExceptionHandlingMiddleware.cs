using BLL.Exceptions;
using System.Net;
using System.Text.Json;

namespace InnoClinic.Auth.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, message) = exception switch
            {
                EmailAlreadyExistsException => (400, exception.Message),
                InvalidPasswordException => (400, exception.Message),
                UserNotFoundException => (400, exception.Message),
                ArgumentNullException _ => (400, "Wrong request parameters"),
                ArgumentException _ => (400, exception.Message),
                KeyNotFoundException _ => (404, exception.Message),
                InvalidOperationException _ => (400, exception.Message),
                _ => (500, "Server error")
            };

            _logger.LogWarning(exception, "Unknown error occured: {Message}", exception.Message);
            context.Response.StatusCode = statusCode;

            var response = new
            {
                error = message
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
