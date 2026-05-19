using System.Diagnostics;

namespace Services.API.Extensions
{
    public static class PresentationExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services) {
            services.AddControllers();
            services.AddOpenApi();
            return services;
        }
    }
}
