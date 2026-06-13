namespace Application.DTOs
{
    public class CreateSpecializationRequestDto
    {
        public string Name { get; set; }
        public IReadOnlyList<Guid> ServiceIds { get; set; }
    }
}
