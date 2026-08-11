using InnoClinic.Documents.API.Application.Interfaces;
using InnoClinic.Documents.API.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InnoClinic.Documents.API.Application.Extensions
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
