using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Appointments.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/appointments")]
    public class AppointmentController(IAppointmentService appointmentService) : ControllerBase
    {

        [HttpPost("createAppointment")]
        [ProducesResponseType(typeof(AppointmentResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequestDto dto)
        {
            var result = await appointmentService.CreateAsync(dto);
            return CreatedAtAction(nameof(CreateAppointment), new { id = result.Id }, result);
        }
    }
}
