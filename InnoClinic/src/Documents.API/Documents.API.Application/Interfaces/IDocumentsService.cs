using InnoClinic.Documents.API.Application.DTOs;

namespace InnoClinic.Documents.API.Application.Interfaces
{
    public interface IDocumentsService
    {
        Task<DocumentDto> UploadAsync(UploadDocumentRequestDto dto, CancellationToken ct = default);
        Task<DocumentDto> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<DocumentDto>> GetAllAsync(CancellationToken ct = default);
        Task<DocumentDto> UpdateAsync(Guid id, UpdateDocumentRequestDto dto, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
