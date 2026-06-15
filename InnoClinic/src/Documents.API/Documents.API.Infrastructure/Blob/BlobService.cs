using Application.Interfaces;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Blob
{
    public class BlobService(BlobServiceClient blobServiceClient) : IBlobService
    {
        public async Task DeleteAsync(string fileUrl, string containerName, CancellationToken ct = default)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobName = Path.GetFileName(new Uri(fileUrl).LocalPath);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync(cancellationToken: ct);
        }

        public async Task<string> UploadAsync(IFormFile file, string containerName, CancellationToken ct = default)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync(cancellationToken: ct);

            var blobName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var blobClient = containerClient.GetBlobClient(blobName);

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: true, cancellationToken: ct);

            return blobClient.Uri.ToString();
        }
    }
}
