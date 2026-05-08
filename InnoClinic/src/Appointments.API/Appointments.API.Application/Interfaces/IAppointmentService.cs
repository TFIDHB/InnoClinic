using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentResponseDto> CreateAsync(CreateAppointmentRequestDto dto, CancellationToken ct = default);
        Task<AppointmentResponseDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}
