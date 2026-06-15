using Domain.Entities;
using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IPhotosRepository : IRepository<Photo, Guid>
    {
    }
}
