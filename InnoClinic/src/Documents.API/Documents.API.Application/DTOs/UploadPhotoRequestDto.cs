using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{
    public class UploadPhotoRequestDto
    {
        public IFormFile File { get; set; }
        public PhotoType Type { get; set; }
    }
}
