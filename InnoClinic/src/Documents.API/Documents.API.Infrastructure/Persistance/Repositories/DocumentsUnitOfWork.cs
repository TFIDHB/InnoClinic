using InnoClinic.Documents.API.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InnoClinic.Documents.API.Infrastructure.Persistance.Repositories
{
    public class DocumentsUnitOfWork(DocumentsDbContext context, IServiceProvider provider): IDocumentsUnitOfWork, IDisposable
    {
        private IPhotosRepository? _photosRepository;
        private IDocumentsRepository? _documentsRepository;

        public IPhotosRepository PhotosRepository =>
            _photosRepository ??= provider.GetRequiredService<IPhotosRepository>();

        public IDocumentsRepository DocumentsRepository =>
            _documentsRepository ??= provider.GetRequiredService<IDocumentsRepository>();

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await context.SaveChangesAsync(ct);

        public void Dispose() => context.Dispose();
    }
}
