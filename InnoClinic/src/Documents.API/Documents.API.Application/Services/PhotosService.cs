using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using InnoClinic.Shared.Exceptions;

namespace Application.Services
{
    public class PhotosService(IDocumentsUnitOfWork unitOfWork, IBlobService blobService, IMapper mapper) : IPhotosService
    {
        private const string containerName = "photos";
        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var photo = await unitOfWork.PhotosRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Photo));
            await blobService.DeleteAsync(photo.Url, containerName, ct);
            await unitOfWork.PhotosRepository.DeleteAsync(id, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<PhotoDto>> GetAllAsync(CancellationToken ct = default)
        {
            var photos = await unitOfWork.PhotosRepository.GetAllAsync(ct);
            return mapper.Map<IEnumerable<PhotoDto>>(photos);
        }

        public async Task<PhotoDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var photo = await unitOfWork.PhotosRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Photo));
            return mapper.Map<PhotoDto>(photo);
        }

        public async Task<PhotoDto> UpdateAsync(Guid id, UpdatePhotoRequestDto dto, CancellationToken ct = default)
        {
            var photo = await unitOfWork.PhotosRepository.GetByIdAsync(id, ct)
                ?? throw new NotFoundException(nameof(Photo));

            await blobService.DeleteAsync(photo.Url, containerName, ct);
            var newUrl = await blobService.UploadAsync(dto.File, containerName, ct);

            photo.Url = newUrl;
            photo.Type = dto.Type;

            await unitOfWork.PhotosRepository.UpdateAsync(photo, ct);
            await unitOfWork.SaveChangesAsync(ct);
            return mapper.Map<PhotoDto>(photo);
        }

        public async Task<PhotoDto> UploadAsync(UploadPhotoRequestDto dto, CancellationToken ct = default)
        {
            var url = await blobService.UploadAsync(dto.File, containerName, ct);
            var photo = new Photo { Url = url, Type = dto.Type };
            await unitOfWork.PhotosRepository.CreateAsync(photo, ct);
            await unitOfWork.SaveChangesAsync(ct);
            return mapper.Map<PhotoDto>(photo);
        }
    }
}
