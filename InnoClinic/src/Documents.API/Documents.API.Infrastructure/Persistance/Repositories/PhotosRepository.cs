using InnoClinic.Documents.API.Application.Interfaces;
using InnoClinic.Documents.API.Domain.Entities;
using InnoClinic.Shared.Repositories;

namespace InnoClinic.Documents.API.Infrastructure.Persistance.Repositories
{
    public class PhotosRepository(DocumentsDbContext context): BaseRepository<Photo, Guid>(context), IPhotosRepository
    {
    }
}
