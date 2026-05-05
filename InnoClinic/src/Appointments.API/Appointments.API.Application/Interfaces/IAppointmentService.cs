using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentResponseDto> CreateAsync(CreateAppointmentRequestDto dto);
    }
}
