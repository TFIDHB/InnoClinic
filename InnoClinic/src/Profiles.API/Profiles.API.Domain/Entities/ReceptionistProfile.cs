namespace Domain.Entities
{
    public class ReceptionistProfile
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public Guid AccountId { get; set; }
        public Guid OfficeId { get; set; }
        public Guid? PhotoId { get; set; }
    }
}
