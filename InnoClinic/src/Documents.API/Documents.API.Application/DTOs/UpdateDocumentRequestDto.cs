using Microsoft.AspNetCore.Http;

namespace InnoClinic.Documents.API.Application.DTOs
{
    public class UpdateDocumentRequestDto
    {
        public required IFormFile File { get; set; }

        public Guid ResultId { get; set; }
    }
}
