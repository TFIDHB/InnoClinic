namespace Application.DTOs
{
    public class AvailableSlotsResponseDto
    {
        public IEnumerable<TimeOnly> AvailableSlots { get; set; }
    }
}
