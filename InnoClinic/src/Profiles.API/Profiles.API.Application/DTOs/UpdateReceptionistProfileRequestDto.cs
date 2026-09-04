namespace Application.DTOs
{
    public class UpdateReceptionistProfileRequestDto
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public string? MiddleName { get; set; }

        public Guid OfficeId { get; set; }
    }
}
