using InnoClinic.Documents.API.Application.DTOs;
using InnoClinic.Documents.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Documents.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/photos")]
    public class PhotosController(IPhotosService photosService) : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PhotoDto>> GetPhoto(Guid id, CancellationToken ct = default)
        {
            var result = await photosService.GetByIdAsync(id, ct);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetAllPhotos(CancellationToken ct = default)
        {
            var result = await photosService.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PhotoDto>> UploadPhoto([FromForm] UploadPhotoRequestDto dto, CancellationToken ct = default)
        {
            var result = await photosService.UploadAsync(dto, ct);
            return CreatedAtAction(nameof(GetPhoto), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PhotoDto>> UpdatePhoto(Guid id, [FromForm] UpdatePhotoRequestDto dto, CancellationToken ct = default)
        {
            var result = await photosService.UpdateAsync(id, dto, ct);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeletePhoto(Guid id, CancellationToken ct = default)
        {
            await photosService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
