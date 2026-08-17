using Application.DTOs;
using Application.Interfaces;
using InnoClinic.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Net.Http.Json;

namespace Infrastructure.Clients
{
    public class DocumentsClient(HttpClient httpClient) : IDocumentsClient
    {
        public async Task<byte[]> DownloadAsync(string url, CancellationToken ct = default)
        {
            return await httpClient.GetByteArrayAsync(url, ct);
        }

        public async Task<DocumentDto?> GetByResultIdAsync(Guid resultId, CancellationToken ct = default)
        {
            var response = await httpClient.GetAsync($"/api/v1/documents/{resultId}", ct);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<DocumentDto>(ct);
        }

        public async Task<DocumentDto> UploadAsync(Guid resultId, byte[] fileBytes, string fileName, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
