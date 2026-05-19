using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddScoped<IServicesService, ServicesService>();
            return services;
        }
    }
}
