using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Profiles.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/patients")]
    public class PatientProfileController(IProfilesService<PatientProfileDto, CreatePatientProfileRequestDto, UpdatePatientProfileRequestDto> patientProfilesService) : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientProfileDto>> GetPatient(Guid id, CancellationToken ct = default)
        {
            var result = await patientProfilesService.GetByIdAsync(id, ct);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<PatientProfileDto>>> GetAllPatients(CancellationToken ct = default)
        {
            var result = await patientProfilesService.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PatientProfileDto>> CreatePatient([FromBody] CreatePatientProfileRequestDto dto, CancellationToken ct = default)
        {
            var result = await patientProfilesService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetPatient), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientProfileDto>> UpdatePatient(Guid id, [FromBody] UpdatePatientProfileRequestDto dto, CancellationToken ct = default)
        {
            var result = await patientProfilesService.UpdateAsync(id, dto, ct);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeletePatient(Guid id, CancellationToken ct = default)
        {
            await patientProfilesService.DeleteAsync(id, ct);
            return Ok();
        }
    }
}