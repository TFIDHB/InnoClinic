using Application.Interfaces;
using Azure.Storage.Blobs;
using Infrastructure.Blob;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Configurations;
using Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DocumentsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DocumentsConnection"));
            });

            services.AddSingleton(new BlobServiceClient(configuration.GetConnectionString("BlobStorage")));
            services.AddScoped<IBlobService, BlobService>();

            services.AddScoped<IPhotosRepository, PhotosRepository>();
            services.AddScoped<IDocumentsRepository, DocumentsRepository>();
            services.AddScoped<IDocumentsUnitOfWork, DocumentsUnitOfWork>();
            services.AddHostedService<DatabaseMigrator>();
            return services;
        }
    }
}
