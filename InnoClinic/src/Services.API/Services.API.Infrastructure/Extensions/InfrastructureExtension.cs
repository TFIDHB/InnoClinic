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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ServicesDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ServicesConnection"));
            });

            services.AddHttpClient<IAppointmentsClient, AppointmentsClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["AppointmentsApi:BaseUrl"]!);
            });

            services.AddScoped<IServicesUnitOfWork, ServicesUnitOfWork>();
            services.AddScoped<IServicesRepository, ServicesRepository>();
            services.AddScoped<ISpecializationsRepository, SpecializationsRepository>();
            return services;
        }
    }
}
