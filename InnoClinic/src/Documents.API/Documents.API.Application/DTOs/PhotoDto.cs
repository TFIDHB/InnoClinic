using InnoClinic.Documents.API.Domain.Enums;

namespace InnoClinic.Documents.API.Application.DTOs
{
    public class PhotoDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public PhotoType Type { get; set; }
    }
}
