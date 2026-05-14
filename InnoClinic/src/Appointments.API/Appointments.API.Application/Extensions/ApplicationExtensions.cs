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
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddAutoMapper(AssemblyReference.Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(AssemblyReference.Assembly);
            return services;
        }
    }
}
