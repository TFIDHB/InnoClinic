using Application.AutoMapper;
using Application.Interfaces;
using Application.Services;
using Application.Validators;
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
            services.AddAutoMapper(typeof(AppointmentMapper));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateAppointmentRequestValidator>();
            return services;
        }
    }
}
