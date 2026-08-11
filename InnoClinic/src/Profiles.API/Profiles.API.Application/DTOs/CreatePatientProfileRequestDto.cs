using Application.Interfaces;

namespace Application.DTOs
{
    public class CreatePatientProfileRequestDto : IPatientFields
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
    }
}
