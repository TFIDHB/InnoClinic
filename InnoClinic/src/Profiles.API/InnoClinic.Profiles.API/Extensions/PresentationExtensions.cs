using InnoClinic.Shared.Extensions;

namespace InnoClinic.Profiles.API.Extensions
{
    public static class PresentationExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services
                .AddAppSwagger("InnoClinic.Profiles.API")
                .AddOpenApi();
            services.AddJwtAuth(configuration);
            return services;
        }
    }
}
