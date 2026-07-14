using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Profiles.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/doctors")]
    public class DoctorProfileController(
        IProfilesService<DoctorProfileDto, CreateDoctorProfileRequestDto, UpdateDoctorProfileRequestDto> doctorProfilesService
        ) : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DoctorProfileDto>> GetDoctor(Guid id, CancellationToken ct = default)
        {
            var result = await doctorProfilesService.GetByIdAsync(id, ct);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<DoctorProfileDto>>> GetAllDoctors(CancellationToken ct = default)
        {
            var result = await doctorProfilesService.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<DoctorProfileDto>> CreateDoctor([FromBody] CreateDoctorProfileRequestDto dto, CancellationToken ct = default)
        {
            var result = await doctorProfilesService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetDoctor), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DoctorProfileDto>> UpdateDoctor(Guid id, [FromBody] UpdateDoctorProfileRequestDto dto, CancellationToken ct = default)
        {
            var result = await doctorProfilesService.UpdateAsync(id, dto, ct);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteDoctor(Guid id, CancellationToken ct = default)
        {
            await doctorProfilesService.DeleteAsync(id, ct);
            return Ok();
        }
    }
}