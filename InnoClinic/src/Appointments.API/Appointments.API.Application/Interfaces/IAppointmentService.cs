using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentResponseDto> CreateAsync(CreateAppointmentRequestDto dto, CancellationToken ct = default);
        Task<AppointmentResponseDto> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<AppointmentSlotDto>> GetSlotsByDateAndDoctorAsync(
            DateOnly date,
            Guid? doctorId,
            CancellationToken ct = default);
        Task<IEnumerable<AppointmentSlotDto>> GetSlotsByDateRangeAndDoctorAsync(
            DateOnly startDate,
            DateOnly endDate,
            Guid? doctorId,
            CancellationToken ct = default);
    }
}
