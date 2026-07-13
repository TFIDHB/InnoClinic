using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Services.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/services")]
    public class ServicesController(IServicesService servicesService) : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ServiceDto>> GetService(Guid id, CancellationToken ct = default)
        {
            var result = await servicesService.GetByIdAsync(id, ct);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAllServices(CancellationToken ct = default) 
        {
            var result = await servicesService.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ServiceDto>> CreateService(
            [FromBody] CreateServiceRequestDto dto,
            CancellationToken ct = default)
        {
            var result = await servicesService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetService), new {id = result.Id}, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ServiceDto>> UpdateService(
            Guid id,
            [FromBody] UpdateServiceRequestDto dto,
            CancellationToken ct = default)
        {
            var result = await servicesService.UpdateAsync(id, dto, ct);
            return Ok(result);
        }

        [HttpPatch("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ServiceDto>> UpdateServiceStatus(
            Guid id,
            [FromBody] UpdateServiceStatusRequestDto dto,
            CancellationToken ct = default)
        {
            var result = await servicesService.UpdateStatusAsync(id, dto, ct);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteService(Guid id, CancellationToken ct = default)
        {
            await servicesService.DeleteAsync(id, ct);
            return NoContent();
        }

        [HttpGet("{id}/time-slot-size")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GetTimeSlotSize(Guid id, CancellationToken ct = default)
        {
            var result = await servicesService.GetTimeSlotSizeAsync(id, ct);
            return Ok(result);
        }
    }
}
