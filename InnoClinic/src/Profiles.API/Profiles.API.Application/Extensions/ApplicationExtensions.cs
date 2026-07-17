using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<PatientProfileService>();
            services.AddScoped<IPatientProfileService>(sp => sp.GetRequiredService<PatientProfileService>());
            services.AddScoped<IProfilesService<PatientProfileDto, CreatePatientProfileRequestDto, UpdatePatientProfileRequestDto>>(sp => sp.GetRequiredService<PatientProfileService>());
            services.AddScoped<IProfilesService<PatientProfileDto, CreatePatientProfileRequestDto, UpdatePatientProfileRequestDto>, PatientProfileService>();
            services.AddScoped<IProfilesService<ReceptionistProfileDto, CreateReceptionistProfileRequestDto, UpdateReceptionistProfileRequestDto>, ReceptionistProfileService>();
            services.AddAutoMapper(AssemblyReference.Assembly);
            return services;
        }
    }
}
