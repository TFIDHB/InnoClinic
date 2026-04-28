using Microsoft.OpenApi.Models;

namespace InnoClinic.Appointments.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddAppSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "InnoClinic.Appointments",
                    Version = "v1",
                });

            });

            return services;
        }

        public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "InnoClinic.Appointments v1");
            });
            return app;
        }
    }
}
