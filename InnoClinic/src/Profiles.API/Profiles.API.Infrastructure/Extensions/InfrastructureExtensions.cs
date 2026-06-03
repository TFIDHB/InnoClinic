using Application.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ProfilesDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ProfilesConnection"));
            });

            service.AddScoped<IProfilesUnitOfWork, ProfilesUnitOfWork>();
            service.AddScoped<IDoctorProfilesRepository, ProfilesRepository>();
            return service;
        }
    }
}
