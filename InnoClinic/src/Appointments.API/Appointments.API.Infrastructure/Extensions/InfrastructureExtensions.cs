using Application.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppointmentDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("AppointmentsConnection"));
            });

            services.AddHttpClient<IServicesClient, ServicesClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ServicesApi:BaseUrl"]!);
            });

            services.AddScoped<IAppointmentUnitOfWork, AppointmentUnitOfWork>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddHostedService<DatabaseMigrator>();
            return services;
        }
    }
}
