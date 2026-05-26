using Domain.Enums;

namespace Application.DTOs
{
    public class UpdateDoctorProfileRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public Guid SpecializationId { get; set; }
        public Guid OfficeId { get; set; }
        public int CareerStartYear { get; set; }
        public DoctorStatus Status { get; set; }
        public Guid? PhotoId { get; set; }
    }
}
