using InnoClinic.Documents.API.Domain.Enums;

namespace InnoClinic.Documents.API.Domain.Entities
{
    public class Photo
    {
        public Guid Id { get; set; }

        public required string Url { get; set; }

        public PhotoType Type { get; set; }
    }
}
