using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using InnoClinic.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Profiles.API.Controllers
{
    //[Authorize(Roles = "Patient,Doctor,Receptionist")]
    [ApiController]
    [Route("api/v1/receptionists")]
    public class ReceptionistProfilesController(
        IProfilesService<ReceptionistProfileDto, CreateReceptionistProfileRequestDto, UpdateReceptionistProfileRequestDto> receptionistProfilesService
        ) : ControllerBase
    {
        [HttpGet("{id}")]
        //[Authorize(Roles = "Receptionist")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReceptionistProfileDto>> GetReceptionist(Guid id, CancellationToken ct = default)
        {
            var result = await receptionistProfilesService.GetByIdAsync(id, ct);
            return Ok(result);
        }

        [HttpGet]
        //[Authorize(Roles = "Receptionist")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ReceptionistProfileDto>>> GetAllReceptionists(CancellationToken ct = default)
        {
            var result = await receptionistProfilesService.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpPost]
        //[Authorize(Roles = "Receptionist")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReceptionistProfileDto>> CreateReceptionist([FromBody] CreateReceptionistProfileRequestDto dto, CancellationToken ct = default)
        {
            var result = await receptionistProfilesService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetReceptionist), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Receptionist")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReceptionistProfileDto>> UpdateReceptionist(Guid id, [FromBody] UpdateReceptionistProfileRequestDto dto, CancellationToken ct = default)
        {
            var result = await receptionistProfilesService.UpdateAsync(id, dto, ct);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Receptionist")]
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