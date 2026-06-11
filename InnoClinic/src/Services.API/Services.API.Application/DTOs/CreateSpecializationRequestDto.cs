namespace Application.DTOs
{
    public class CreateSpecializationRequestDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<Guid> ServiceIds { get; set; }
    }
}
