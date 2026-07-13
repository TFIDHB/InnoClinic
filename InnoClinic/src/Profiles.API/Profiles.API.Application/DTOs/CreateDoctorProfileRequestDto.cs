using Domain.Enums;

namespace Application.DTOs
{
    public class CreateDoctorProfileRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public Guid AccountId { get; set; }

        /// <example>a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1</example>
        public Guid SpecializationId { get; set; }
        public Guid OfficeId { get; set; }
        public int CareerStartYear { get; set; }
        public DoctorStatus Status { get; set; }
    }
}
