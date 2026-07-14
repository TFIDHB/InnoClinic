using InnoClinic.Documents.API.Application.Interfaces;
using InnoClinic.Documents.API.Domain.Entities;
using InnoClinic.Shared.Repositories;

namespace InnoClinic.Documents.API.Infrastructure.Persistance.Repositories
{
    public class DocumentsRepository(DocumentsDbContext context) : BaseRepository<Document, Guid>(context), IDocumentsRepository
    {
    }
}
