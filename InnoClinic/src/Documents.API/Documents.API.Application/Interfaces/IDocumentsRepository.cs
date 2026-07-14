using InnoClinic.Documents.API.Domain.Entities;
using InnoClinic.Shared.Interfaces;

namespace InnoClinic.Documents.API.Application.Interfaces
{
    public interface IDocumentsRepository : IRepository<Document, Guid>
    {
    }
}
