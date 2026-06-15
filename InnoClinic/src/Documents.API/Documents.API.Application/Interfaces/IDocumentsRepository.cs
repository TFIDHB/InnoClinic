using Domain.Entities;
using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IDocumentsRepository : IRepository<Document, Guid>
    {
    }
}
