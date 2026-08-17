using InnoClinic.Documents.API.Application.DTOs;
using InnoClinic.Documents.API.Domain.Entities;
using InnoClinic.Shared.Interfaces;

namespace InnoClinic.Documents.API.Application.Interfaces
{
    public interface IDocumentsRepository : IRepository<Document, Guid>
    {
        Task<Document?> GetByResultIdAsync(Guid resultId, CancellationToken ct = default);
    }
}
