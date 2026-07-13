using Documents.API.Domain.Entities;
using InnoClinic.Shared.Interfaces;

namespace Documents.API.Application.Interfaces
{
    public interface IPhotosRepository : IRepository<Photo, Guid>
    {
    }
}
