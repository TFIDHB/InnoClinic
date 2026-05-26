namespace Application.DTOs
{
    public class UpdatePatientProfileRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? PhotoId { get; set; }
    }
}
