namespace InnoClinic.Appointments.Extensions
{
    public static class PresentationExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services) {
            services.AddControllers();
            services.AddAppSwagger();
            return services;
        }
    }
}
