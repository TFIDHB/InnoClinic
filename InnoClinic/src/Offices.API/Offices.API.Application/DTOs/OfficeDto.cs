namespace Application.DTOs
{
    public class OfficeDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public Guid PhotoId { get; set; }
        public string RegistryPhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
