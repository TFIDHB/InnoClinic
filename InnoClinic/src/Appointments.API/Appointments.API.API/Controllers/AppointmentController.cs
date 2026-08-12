using Application.DTOs;
using Application.Interfaces;
using InnoClinic.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Appointments.API.Controllers
{
    [Authorize (Roles = Roles.AllRoles)]
    [ApiController]
    [Route("api/v1/appointments")]
    public class AppointmentController(IAppointmentService appointmentService) : ControllerBase
    {
        [HttpGet("{id}")]
        [Authorize (Roles = Roles.AllRoles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AppointmentResponseDto>> GetAppointment(Guid id)
        {
            var result = await appointmentService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize (Roles = Roles.Receptionist)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AppointmentResponseDto>> CreateAppointment(
            [FromBody] CreateAppointmentRequestDto dto,
            CancellationToken ct = default)
        {
            var result = await appointmentService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetAppointment), new { id = result.Id }, result);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Patient + "," + Roles.Receptionist)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AppointmentSlotDto>>> GetAppointmentSlots(
            [FromQuery] DateOnly date,
            [FromQuery] Guid? doctorId,
            CancellationToken ct = default)
        {
            var result = await appointmentService.GetSlotsByDateAndDoctorAsync(date, doctorId, ct);
            return Ok(result);
        }

        [HttpGet("range")]
        [Authorize(Roles = Roles.Patient + "," + Roles.Receptionist)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAppointmentsByRange(
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate,
            [FromQuery] Guid? doctorId,
            CancellationToken ct = default)
        {
            var result = await appointmentService.GetSlotsByDateRangeAndDoctorAsync(startDate, endDate, doctorId, ct);
            return Ok(result);
        }
    }
}
