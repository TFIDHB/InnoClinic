using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IServicesService, ServicesService>();
            services.AddScoped<ISpecializationsService, SpecializationsService>();
            services.AddAutoMapper(AssemblyReference.Assembly);
            return services;
        }
    }
}
