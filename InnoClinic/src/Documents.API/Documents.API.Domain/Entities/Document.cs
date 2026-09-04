namespace InnoClinic.Documents.API.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; set; }

        public required string Url { get; set; }

        public Guid ResultId { get; set; }
    }
}
