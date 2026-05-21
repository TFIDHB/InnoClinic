using InnoClinic.Shared.Extensions;

namespace InnoClinic.Profiles.API.Extensions
{
    public static class PresentationExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services
                .AddAppSwagger("InnoClinic.Profiles.API")
                .AddOpenApi();
            return services;
        }
    }
}
