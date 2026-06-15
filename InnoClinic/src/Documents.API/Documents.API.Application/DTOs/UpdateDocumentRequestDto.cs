using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{
    public class UpdateDocumentRequestDto
    {
        public IFormFile File { get; set; }
        public Guid ResultId { get; set; }
    }
}
