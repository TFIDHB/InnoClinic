using InnoClinic.Documents.API.Application.DTOs;

namespace InnoClinic.Documents.API.Application.Interfaces
{
    public interface IPhotosService
    {
        Task<PhotoDto> UploadAsync(UploadPhotoRequestDto dto, CancellationToken ct = default);
        Task<PhotoDto> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<PhotoDto>> GetAllAsync(CancellationToken ct = default);
        Task<PhotoDto> UpdateAsync(Guid id, UpdatePhotoRequestDto dto, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
