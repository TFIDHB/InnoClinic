using InnoClinic.Shared.Extensions;

namespace InnoClinic.Documents.API.Extensions
{
    public static class PresentationExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services
                .AddAppSwagger("InnoClinic.Documents.API")
                .AddOpenApi();
            return services;
        }
    }
}
