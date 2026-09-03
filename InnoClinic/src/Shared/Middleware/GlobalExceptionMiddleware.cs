using System.Text.Json;
using InnoClinic.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace InnoClinic.Shared.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
                BadRequestException => (400, exception.Message),
                UnauthorizedAccessException => (401, "Unauthorized"),
                ForbiddenException => (403, exception.Message),
                NotFoundException => (404, exception.Message),
                ExternalServiceException => (502, exception.Message),
                _ => (500, "Internal Server Error")
            };

            _logger.LogError(exception, "Error captured by middleware: {Message}", exception.Message);
            context.Response.StatusCode = statusCode;

            var response = new { error = message };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}