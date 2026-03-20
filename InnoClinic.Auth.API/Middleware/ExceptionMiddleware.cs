using BLL.Exceptions;
using System.Net;

namespace InnoClinic.Auth.API.Middleware
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (EmailAlreadyExistsException ex) {
                _logger.LogWarning(ex, "Email already exists: {Message}", ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Unknown error occured: {Message}", ex.Message);
            }
        }
    }
}
