using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAccountSearchService, AccountSearchService>();
            services.AddScoped<IProfilesService<PatientProfileDto, CreatePatientProfileRequestDto, UpdatePatientProfileRequestDto>>(sp => sp.GetRequiredService<PatientProfileService>());
            services.AddScoped<IProfilesService<DoctorProfileDto, CreateDoctorProfileRequestDto, UpdateDoctorProfileRequestDto>>(sp => sp.GetRequiredService<DoctorProfileService>());
            services.AddScoped<IProfilesService<ReceptionistProfileDto, CreateReceptionistProfileRequestDto, UpdateReceptionistProfileRequestDto>, ReceptionistProfileService>();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(AssemblyReference.Assembly);
            services.AddAutoMapper(AssemblyReference.Assembly);
            return services;
        }
    }
}
