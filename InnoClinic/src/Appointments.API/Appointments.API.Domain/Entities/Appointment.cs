using Domain.Enums;

namespace Domain.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid SpecializationId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid OfficeId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime StartDateTime => Date.ToDateTime(Time);
        public DateTime EndDateTime => StartDateTime.Add(Duration);

        public AppointmentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
