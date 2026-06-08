using InnoClinic.Shared.Extensions;

namespace Offices.API.Extensions
{
    public static class PresentationExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services) 
        {
            services.AddControllers();
            services
                .AddAppSwagger("InnoClinic.Offices.API")
                .AddOpenApi();
            return services;
        }
    }
}
