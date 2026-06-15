using Domain.Enums;

namespace Domain.Entities
{
    public class Photo
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public PhotoType Type { get; set; }
    }
}
