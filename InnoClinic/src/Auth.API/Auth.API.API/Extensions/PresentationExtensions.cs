using FluentValidation;
using FluentValidation.AspNetCore;
using InnoClinic.Shared.Extensions;

namespace InnoClinic.Auth.API.Extensions
{
    public static class PresentationExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            services.AddOpenApi();
            services.AddAppSwagger("InnoClinic.Auth.API");
            services.AddJwtAuth(configuration);
            return services;
        }
    }
}
