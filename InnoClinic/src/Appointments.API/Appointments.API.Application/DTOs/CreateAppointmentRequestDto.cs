using Domain.Enums;

namespace Application.DTOs
{
    public class CreateAppointmentRequestDto
    {
        public Guid PatientId { get; set; }
        public Guid SpecializationId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid OfficeId { get; set; }

        /// <example>2026-05-20</example>
        public DateOnly Date { get; set; }

        /// <example>14:30</example>
        public TimeOnly Time { get; set; }
    }
}
