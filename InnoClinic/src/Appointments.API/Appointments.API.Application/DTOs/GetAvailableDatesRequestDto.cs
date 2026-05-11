using Domain.Enums;

namespace Application.DTOs
{
    public class GetAvailableDatesRequestDto
    {
        public Guid SpecialisationId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid? DoctorId { get; set; }

        /// <example>1</example>
        public ServiceType ServiceType { get; set; }
    }
}
