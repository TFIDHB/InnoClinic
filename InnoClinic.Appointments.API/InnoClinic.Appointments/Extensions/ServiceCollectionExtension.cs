using Application.AutoMapper;
using Application.Interfaces;
using Application.Services;
using Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Persistence.Repositories;

namespace InnoClinic.Appointments.Extensions
{
    static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped(typeof(IRepository<,>), typeof(BasicRepository<,>));
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddAutoMapper(typeof(AppointmentMapper));
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateAppointmentRequestValidator>();
            return services;
        }
    }
}
