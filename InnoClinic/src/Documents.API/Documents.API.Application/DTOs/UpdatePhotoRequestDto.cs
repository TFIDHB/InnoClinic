using InnoClinic.Documents.API.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace InnoClinic.Documents.API.Application.DTOs
{
    public class UpdatePhotoRequestDto
    {
        public IFormFile File { get; set; }
        public PhotoType Type { get; set; }
    }
}
