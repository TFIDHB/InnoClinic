using InnoClinic.Shared.Extensions;

namespace Services.API.Extensions
{
    public static class PresentationExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddAppSwagger("InnoClinic.Services.API");
            services.AddOpenApi();
            return services;
        }
    }
}
