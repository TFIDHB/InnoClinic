using Domain.Enums;

namespace Application.DTOs
{
    public class GetAvailableSlotsRequestDto
    {
        /// <example>2026-05-20</example>
        public DateOnly Date { get; set; }
        public Guid SpecialisationId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid? DoctorId { get; set; }

        /// <example>1</example>
        public ServiceType ServiceType { get; set; }
    }
}
