using Application.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ServicesDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ServicesConnection"));
            });

            service.AddScoped<IServicesUnitOfWork, ServicesUnitOfWork>();
            service.AddScoped<IServicesRepository, ServicesRepository>();
            return service;
        }
    }
}
