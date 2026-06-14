namespace Application.DTOs
{
    public class UpdateSpecializationRequestDto
    {
        public string Name { get; set; }
        public IReadOnlyList<Guid> ServiceIds { get; set; }
    }
}
