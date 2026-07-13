using Application.Interfaces;
using Application.Options;
using Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<WorkingHoursOptions>(configuration.GetSection("WorkingHours"));
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddAutoMapper(AssemblyReference.Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(AssemblyReference.Assembly);
            return services;
        }
    }
}
