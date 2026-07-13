using Documents.API.Application.DTOs;
using Documents.API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Documents.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/documents")]
    public class DocumentsController(IDocumentsService documentsService) : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DocumentDto>> GetDocument(Guid id, CancellationToken ct = default)
        {
            var result = await documentsService.GetByIdAsync(id, ct);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetAllDocuments(CancellationToken ct = default)
        {
            var result = await documentsService.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<DocumentDto>> UploadDocument([FromForm] UploadDocumentRequestDto dto, CancellationToken ct = default)
        {
            var result = await documentsService.UploadAsync(dto, ct);
            return CreatedAtAction(nameof(GetDocument), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DocumentDto>> UpdateDocument(Guid id, [FromForm] UpdateDocumentRequestDto dto, CancellationToken ct = default)
        {
            var result = await documentsService.UpdateAsync(id, dto, ct);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteDocument(Guid id, CancellationToken ct = default)
        {
            await documentsService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
