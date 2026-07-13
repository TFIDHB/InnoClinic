using Documents.API.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Documents.API.Application.DTOs
{
    public class UpdatePhotoRequestDto
    {
        public IFormFile File { get; set; }
        public PhotoType Type { get; set; }
    }
}
