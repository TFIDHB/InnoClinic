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
                EmailAlreadyExistsException => ((int)HttpStatusCode.BadRequest, exception.Message),
                InvalidPasswordException => ((int)HttpStatusCode.BadRequest, exception.Message),
                UserNotFoundException => ((int)HttpStatusCode.NotFound, exception.Message),
                ArgumentNullException _ => ((int)HttpStatusCode.BadRequest, "Wrong request parameters"),
                ArgumentException _ => ((int)HttpStatusCode.BadRequest, exception.Message),
                KeyNotFoundException _ => ((int)HttpStatusCode.NotFound, exception.Message),
                InvalidOperationException _ => ((int)HttpStatusCode.BadRequest, exception.Message),
                _ => ((int)HttpStatusCode.InternalServerError, "Server error")
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
