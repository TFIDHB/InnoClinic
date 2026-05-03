using Microsoft.OpenApi.Models;
using System.Reflection;

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

                var apiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
                if (File.Exists(apiXmlPath))
                {
                    c.IncludeXmlComments(apiXmlPath);
                }

                var appXmlPath = Path.Combine(AppContext.BaseDirectory, "Application.xml");
                if (File.Exists(appXmlPath))
                {
                    c.IncludeXmlComments(appXmlPath);
                }

            });

            return services;
        }

        public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "InnoClinic.Appointments v1");
                c.RoutePrefix = string.Empty;
            });
            return app;
        }
    }
}
