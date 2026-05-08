using InnoClinic.Shared.Extensions;

namespace InnoClinic.Appointments.Extensions
{
    public static class PresentationExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services) {
            services.AddControllers();
            services.AddAppSwagger("InnoClinic.Appointments.API");
            return services;
        }
    }
}
