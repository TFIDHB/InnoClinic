using InnoClinic.Documents.API.Application.Interfaces;
using InnoClinic.Documents.API.Domain.Entities;
using InnoClinic.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Documents.API.Infrastructure.Persistance.Repositories
{
    public class DocumentsRepository(DocumentsDbContext context): BaseRepository<Document, Guid>(context), IDocumentsRepository
    {
        public async Task<Document?> GetByResultIdAsync(Guid resultId, CancellationToken ct = default)
        {
            return await context.Documents.FirstOrDefaultAsync(d => d.ResultId == resultId, ct);
        }
    }
}
