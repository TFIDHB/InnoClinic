using Application.Interfaces;
using Domain.Entities;
using InnoClinic.Shared.Repositories;

namespace Infrastructure.Persistance.Repositories
{
    public class PhotosRepository(DocumentsDbContext context) : BaseRepository<Photo, Guid>(context), IPhotosRepository
    {
    }
}
