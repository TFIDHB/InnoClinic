namespace Application.DTOs
{
    public class SpecializationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public IReadOnlyList<ServiceDto> Services { get; set; }
    }
}
