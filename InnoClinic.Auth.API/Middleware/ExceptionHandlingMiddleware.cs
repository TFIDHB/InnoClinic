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
            catch (EmailAlreadyExistsException ex) {
                _logger.LogWarning(ex, "Email already exists: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Unknown error occured: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, message) = exception switch
            {
                ArgumentNullException _ => (400, "Wrong request parameters"),
                ArgumentException _ => (400, exception.Message),
                KeyNotFoundException _ => (404, exception.Message),
                InvalidOperationException _ => (400, exception.Message),
                _ => (500, "Server error")
            };

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
