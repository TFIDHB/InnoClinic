using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Appointments.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("createAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequestDto dto)
        {
            var result = await _appointmentService.CreateAsync(dto);
            return Ok(result);
        }
    }
}
