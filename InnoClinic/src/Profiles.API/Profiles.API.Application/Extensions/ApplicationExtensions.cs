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
            services.AddScoped<IDoctorProfileService, DoctorProfileService>();
            services.AddScoped<IPatientProfileService, PatientProfileService>();
            services.AddScoped<IAccountSearchService, AccountSearchService>();

            services.AddScoped<IProfilesService<ReceptionistProfileDto, CreateReceptionistProfileRequestDto, UpdateReceptionistProfileRequestDto>, ReceptionistProfileService>();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(AssemblyReference.Assembly);
            services.AddAutoMapper(AssemblyReference.Assembly);
            return services;
        }
    }
}
