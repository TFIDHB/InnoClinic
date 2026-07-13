using Microsoft.AspNetCore.Http;

namespace Documents.API.Application.Interfaces
{
    public interface IBlobService
    {
        Task<string> UploadDocumentAsync(IFormFile file, CancellationToken ct = default);
        Task<string> UploadPhotoAsync(IFormFile file, CancellationToken ct = default);
        Task DeleteAsync(string fileUrl, CancellationToken ct = default);
    }
}
