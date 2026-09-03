using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace InnoClinic.Shared.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddAppSwagger(this IServiceCollection services, string title, string version = "v1", bool includeAuth = true)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = title,
                    Version = version,
                });

                var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
                foreach (var xmlFile in xmlFiles)
                {
                    c.IncludeXmlComments(xmlFile);
                }

                if (includeAuth)
                {
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                            },
                            Array.Empty<string>()
                        },
                    });
                }
            });
            return services;
        }

        public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder app, string title, string version = "v1")
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{title} {version}");
                c.RoutePrefix = string.Empty;
            });
            return app;
        }
    }
}
