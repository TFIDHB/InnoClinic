using Application.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using InnoClinic.Shared.Migrators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            return services;
        }
    }
}
