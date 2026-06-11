namespace Application.DTOs
{
    public class SpecializationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<ServiceDto> Services { get; set; }
    }
}
