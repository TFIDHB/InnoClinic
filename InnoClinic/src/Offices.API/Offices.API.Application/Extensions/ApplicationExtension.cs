using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            services.AddScoped<IOfficesService, OfficesService>();
            services.AddAutoMapper(AssemblyReference.Assembly);
            return services;
        }
    }
}
