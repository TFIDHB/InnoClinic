using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPhotosService, PhotosService>();
            services.AddScoped<IDocumentsService, DocumentsService>();
            services.AddAutoMapper(AssemblyReference.Assembly);
            return services;
        }
    }
}
