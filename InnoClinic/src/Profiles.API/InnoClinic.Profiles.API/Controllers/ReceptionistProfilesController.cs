using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Profiles.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/receptionists")]
    public class ReceptionistProfilesController(IProfilesService<ReceptionistProfileDto, CreateReceptionistProfileRequestDto, UpdateReceptionistProfileRequestDto> receptionistProfilesService) : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReceptionistProfileDto>> GetReceptionist(Guid id, CancellationToken ct = default)
        {
            var result = await receptionistProfilesService.GetByIdAsync(id, ct);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ReceptionistProfileDto>>> GetAllReceptionists(CancellationToken ct = default)
        {
            var result = await receptionistProfilesService.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReceptionistProfileDto>> CreateReceptionist([FromBody] CreateReceptionistProfileRequestDto dto, CancellationToken ct = default)
        {
            var result = await receptionistProfilesService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetReceptionist), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReceptionistProfileDto>> UpdateReceptionist(Guid id, [FromBody] UpdateReceptionistProfileRequestDto dto, CancellationToken ct = default)
        {
            var result = await receptionistProfilesService.UpdateAsync(id, dto, ct);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteReceptionist(Guid id, CancellationToken ct = default)
        {
            await receptionistProfilesService.DeleteAsync(id, ct);
            return Ok();
        }
    }
}