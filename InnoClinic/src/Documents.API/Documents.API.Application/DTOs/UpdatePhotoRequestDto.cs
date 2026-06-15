using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{
    public class UpdatePhotoRequestDto
    {
        public IFormFile File { get; set; }
        public PhotoType Type { get; set; }
    }
}
