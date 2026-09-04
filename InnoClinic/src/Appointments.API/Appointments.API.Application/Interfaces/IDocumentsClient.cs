using Application.DTOs;

namespace Application.Interfaces
{
    public interface IDocumentsClient
    {
        Task<DocumentDto?> GetByResultIdAsync(Guid resultId, CancellationToken ct = default);

        Task<DocumentDto> UploadAsync(Guid resultId, byte[] fileBytes, string fileName, CancellationToken ct = default);

        Task<byte[]> DownloadAsync(string url, CancellationToken ct = default);
    }
}
