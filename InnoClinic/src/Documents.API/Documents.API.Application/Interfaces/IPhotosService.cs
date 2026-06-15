using Application.DTOs;

namespace Application.Interfaces
{
    public interface IPhotosService
    {
        Task<PhotoDto> UploadAsync(UploadPhotoRequestDto dto, CancellationToken ct = default);
        Task<PhotoDto> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<PhotoDto>> GetAllAsync(CancellationToken ct = default);
        Task<PhotoDto> UpdateAsync(Guid id, UploadPhotoRequestDto dto, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
