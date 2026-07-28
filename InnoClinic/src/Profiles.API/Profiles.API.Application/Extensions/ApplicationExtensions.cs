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
            services.AddScopedSp<PatientProfileService,
                IPatientProfileService,
                IProfilesService<PatientProfileDto, CreatePatientProfileRequestDto, UpdatePatientProfileRequestDto>>();

            services.AddScopedSp<DoctorProfileService,
                IDoctorProfileService,
                IProfilesService<DoctorProfileDto, CreateDoctorProfileRequestDto, UpdateDoctorProfileRequestDto>>();

            services.AddScoped<IProfilesService<PatientProfileDto, CreatePatientProfileRequestDto, UpdatePatientProfileRequestDto>>(sp => sp.GetRequiredService<PatientProfileService>());
            services.AddScoped<IProfilesService<DoctorProfileDto, CreateDoctorProfileRequestDto, UpdateDoctorProfileRequestDto>>(sp => sp.GetRequiredService<DoctorProfileService>());
            services.AddScoped<IProfilesService<ReceptionistProfileDto, CreateReceptionistProfileRequestDto, UpdateReceptionistProfileRequestDto>, ReceptionistProfileService>();
            services.AddAutoMapper(AssemblyReference.Assembly);
            return services;
        }
    }
}
