using Documents.API.Application.Interfaces;
using Documents.API.Domain.Entities;
using InnoClinic.Shared.Repositories;

namespace Documents.API.Infrastructure.Persistance.Repositories
{
    public class PhotosRepository(DocumentsDbContext context) : BaseRepository<Photo, Guid>(context), IPhotosRepository
    {
    }
}
