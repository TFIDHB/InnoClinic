using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceCollectionScopedExtension
    {
        public static IServiceCollection AddScopedSp<TClass, TInterface1, TInterface2>(this IServiceCollection services)
            where TClass : class, TInterface1, TInterface2
            where TInterface1 : class
            where TInterface2 : class
        {
            services.AddScoped<TClass>();
            services.AddScoped<TInterface1>(sp => sp.GetRequiredService<TClass>());
            services.AddScoped<TInterface2>(sp => sp.GetRequiredService<TClass>());
            return services;
        }
    }
}
