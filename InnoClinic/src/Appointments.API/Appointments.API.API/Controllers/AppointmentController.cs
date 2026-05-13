using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Appointments.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/appointments")]
    public class AppointmentController(IAppointmentService appointmentService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(Guid id)
        {
            var result = await appointmentService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AppointmentResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequestDto dto, CancellationToken ct = default)
        {
            var result = await appointmentService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetAppointment), new { id = result.Id }, result);
        }

        [HttpGet("dates")]
        [ProducesResponseType(typeof(AvailableDatesResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAvailableDates([FromQuery] GetAvailableDatesRequestDto dto, CancellationToken ct = default)
        {
            var result = await appointmentService.GetAvailableDatesAsync(dto, ct);
            return Ok(new AvailableDatesResponseDto { AvailableDates = result });
        }

        [HttpGet("slots")]
        [ProducesResponseType(typeof(AvailableSlotsResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAvailableSlots([FromQuery] GetAvailableSlotsRequestDto dto, CancellationToken ct = default)
        {
            var result = await appointmentService.GetAvailableSlotsAsync(dto, ct);
            return Ok(new AvailableSlotsResponseDto { AvailableSlots = result });
        }
    }
}
