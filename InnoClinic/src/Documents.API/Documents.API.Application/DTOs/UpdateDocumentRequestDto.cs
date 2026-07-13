using Microsoft.AspNetCore.Http;

namespace Documents.API.Application.DTOs
{
    public class UpdateDocumentRequestDto
    {
        public IFormFile File { get; set; }
        public Guid ResultId { get; set; }
    }
}
