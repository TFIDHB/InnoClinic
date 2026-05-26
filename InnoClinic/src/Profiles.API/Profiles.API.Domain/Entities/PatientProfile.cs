namespace Domain.Entities
{
    public class PatientProfile
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? AccountId { get; set; }
        public bool IsLinkedToAccount { get; set; }
        public Guid? PhotoId { get; set; }
    }
}
