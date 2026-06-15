using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using InnoClinic.Shared.Exceptions;

namespace Application.Services
{
    public class DocumentsService(IDocumentsUnitOfWork unitOfWork, IBlobService blobService, IMapper mapper) : IDocumentsService
    {
        private const string _containerName = "documents";

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var document = await unitOfWork.DocumentsRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Document));

            await blobService.DeleteAsync(document.Url, _containerName, ct);
            await unitOfWork.DocumentsRepository.DeleteAsync(id, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<DocumentDto>> GetAllAsync(CancellationToken ct = default)
        {
            var documents = await unitOfWork.DocumentsRepository.GetAllAsync(ct);
            return mapper.Map<IEnumerable<DocumentDto>>(documents);
        }

        public async Task<DocumentDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var document = await unitOfWork.DocumentsRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Document));
            return mapper.Map<DocumentDto>(document);
        }

        public async Task<DocumentDto> UpdateAsync(Guid id, UpdateDocumentRequestDto dto, CancellationToken ct = default)
        {
            var document = await unitOfWork.DocumentsRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Document));

            await blobService.DeleteAsync(document.Url, _containerName, ct);
            var newUrl = await blobService.UploadAsync(dto.File, _containerName, ct);

            document.Url = newUrl;
            document.ResultId = dto.ResultId;

            await unitOfWork.DocumentsRepository.UpdateAsync(document, ct);
            await unitOfWork.SaveChangesAsync(ct);
            return mapper.Map<DocumentDto>(document);
        }

        public async Task<DocumentDto> UploadAsync(UploadDocumentRequestDto dto, CancellationToken ct = default)
        {
            var url = await blobService.UploadAsync(dto.File, _containerName, ct);
            var document = new Document { Url = url, ResultId = dto.ResultId };
            await unitOfWork.DocumentsRepository.CreateAsync(document, ct);
            await unitOfWork.SaveChangesAsync(ct);
            return mapper.Map<DocumentDto>(document);
        }
    }
}
