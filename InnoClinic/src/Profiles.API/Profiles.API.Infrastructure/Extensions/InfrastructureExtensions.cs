using Application.Interfaces;
using Infrastructure.Options;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using InnoClinic.Shared.Migrators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProfilesDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ProfilesConnection"));
            });

            services.AddScoped<IProfilesUnitOfWork, ProfilesUnitOfWork>();
            services.AddScoped<IDoctorProfilesRepository, DoctorProfilesRepository>();
            services.AddScoped<IPatientProfilesRepository, PatientProfilesRepository>();
            services.AddScoped<IReceptionistProfilesRepository, ReceptionistProfilesRepository>();
            services.AddHostedService<DatabaseMigrator<ProfilesDbContext>>();

            services.Configure<AuthApiOptions>(configuration.GetSection("AuthApi"));
            services.AddHttpClient<IAuthClient, AuthClient>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<AuthApiOptions>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl);
            });
            return services;
        }
    }
}
