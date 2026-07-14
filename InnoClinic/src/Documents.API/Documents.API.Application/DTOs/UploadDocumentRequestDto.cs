using Microsoft.AspNetCore.Http;

namespace InnoClinic.Documents.API.Application.DTOs
{
    public class UploadDocumentRequestDto
    {
        public IFormFile File { get; set; }
        public Guid ResultId { get; set; }
    }
}
