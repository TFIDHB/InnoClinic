using Azure.Storage.Blobs;
using InnoClinic.Documents.API.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace InnoClinic.Documents.API.Infrastructure.Blob
{
    public class BlobService(BlobServiceClient blobServiceClient): IBlobService
    {
        private const string _documentsContainer = "documents";
        private const string _photosContainer = "photos";

        public async Task<string> UploadDocumentAsync(IFormFile file, CancellationToken ct = default)
        => await UploadInternalAsync(file, _documentsContainer, ct);

        public async Task<string> UploadPhotoAsync(IFormFile file, CancellationToken ct = default)
            => await UploadInternalAsync(file, _photosContainer, ct);

        public async Task DeleteAsync(string fileUrl, CancellationToken ct = default)
        {
            var blobUriBuilder = new BlobUriBuilder(new Uri(fileUrl));
            var containerClient = blobServiceClient.GetBlobContainerClient(blobUriBuilder.BlobContainerName);
            var blobClient = containerClient.GetBlobClient(blobUriBuilder.BlobName);
            await blobClient.DeleteIfExistsAsync(cancellationToken: ct);
        }

        private async Task<string> UploadInternalAsync(IFormFile file, string containerName, CancellationToken ct)
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
