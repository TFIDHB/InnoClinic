using Application.Interfaces;
using Domain.Entities;
using InnoClinic.Shared.Repositories;

namespace Infrastructure.Persistance.Repositories
{
    public class DocumentsRepository(DocumentsDbContext context) : BaseRepository<Document, Guid>(context), IDocumentsRepository
    {
    }
}
