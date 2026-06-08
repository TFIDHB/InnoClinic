using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Offices.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/offices")]
    public class OfficesController(IOfficesService officesService) : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OfficeDto>> GetOffice(Guid id, CancellationToken ct = default)
        {
            var result = await officesService.GetByIdAsync(id, ct);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<OfficeDto>>> GetAllOffices(CancellationToken ct = default)
        {
            var result = await officesService.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OfficeDto>> CreateOffice([FromBody] CreateOfficeRequestDto dto, CancellationToken ct = default)
        {
            var result = await officesService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetOffice), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OfficeDto>> UpdateOffice(Guid id, [FromBody] UpdateOfficeRequestDto dto, CancellationToken ct = default)
        {
            var result = await officesService.UpdateAsync(id, dto, ct);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteOffice(Guid id, CancellationToken ct = default)
        {
            await officesService.DeleteAsync(id, ct);
            return Ok();
        }
    }
}
