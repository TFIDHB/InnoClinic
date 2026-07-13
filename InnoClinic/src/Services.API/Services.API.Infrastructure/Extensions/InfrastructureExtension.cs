using Application.Interfaces;
using Infrastructure.Options;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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

            services.Configure<AppointmentsApiOptions>(configuration.GetSection("AppointmentsApi"));

            services.AddHttpClient<IAppointmentsClient, AppointmentsClient>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<AppointmentsApiOptions>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl);
            });

            services.AddScoped<IServicesUnitOfWork, ServicesUnitOfWork>();
            services.AddScoped<IServicesRepository, ServicesRepository>();
            services.AddScoped<ISpecializationsRepository, SpecializationsRepository>();
            services.AddHostedService<DatabaseMigrator>();
            return services;
        }
    }
}
