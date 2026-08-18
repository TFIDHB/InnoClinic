using InnoClinic.Documents.API.Application.DTOs;
using InnoClinic.Documents.API.Application.Interfaces;
using InnoClinic.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Documents.API.Controllers
{
    [Authorize(Roles = Roles.AllRoles)]
    [ApiController]
    [Route("api/v1/documents")]
    public class DocumentsController(IDocumentsService documentsService) : ControllerBase
    {
        [HttpGet("{id}")]
        [Authorize(Roles = Roles.AllRoles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocumentDto>> GetDocument(Guid id, CancellationToken ct = default)
        {
            var result = await documentsService.GetByIdAsync(id, ct);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = Roles.AllRoles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetAllDocuments(CancellationToken ct = default)
        {
            var result = await documentsService.GetAllAsync(ct);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Doctor)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocumentDto>> UploadDocument([FromForm] UploadDocumentRequestDto dto, CancellationToken ct = default)
        {
            var result = await documentsService.UploadAsync(dto, ct);
            return CreatedAtAction(nameof(GetDocument), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Doctor)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocumentDto>> UpdateDocument(Guid id, [FromForm] UpdateDocumentRequestDto dto, CancellationToken ct = default)
        {
            var result = await documentsService.UpdateAsync(id, dto, ct);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Doctor)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteDocument(Guid id, CancellationToken ct = default)
        {
            await documentsService.DeleteAsync(id, ct);
            return NoContent();
        }

        [HttpGet("by-result/{resultId}")]
        [Authorize(Roles = Roles.AllRoles)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocumentDto>> GetDocumentByResultId(Guid resultId, CancellationToken ct = default)
        {
            var result = await documentsService.GetByResultIdAsync(resultId, ct);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
