namespace Application.DTOs
{
    public class UpdateSpecializationRequestDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<Guid> ServiceIds { get; set; }
    }
}
