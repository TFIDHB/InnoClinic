namespace Application.DTOs
{
    public class AppointmentSlotDto
    {
        public TimeOnly Time { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
