using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IBlobService
    {
        Task<string> UploadAsync(IFormFile file, string containerName, CancellationToken ct = default);
        Task DeleteAsync(string fileUrl, string containerName, CancellationToken ct = default);
    }
}
