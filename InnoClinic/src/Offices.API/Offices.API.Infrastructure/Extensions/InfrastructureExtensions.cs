using Application.Interfaces;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<OfficesDbContext>();
            services.AddScoped<IOfficesRepository, OfficesRepository>();
            return services;
        }
    }
}
