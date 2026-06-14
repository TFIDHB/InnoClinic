using Application.DTOs;

namespace Application.Interfaces
{
    public interface IServicesService
    {
        Task<ServiceDto> CreateAsync(CreateServiceRequestDto dto, CancellationToken ct = default);
        Task<ServiceDto> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<ServiceDto>> GetAllAsync(CancellationToken ct = default);
        Task<ServiceDto> UpdateAsync(Guid id, UpdateServiceRequestDto dto, CancellationToken ct = default);
        Task<ServiceDto> UpdateStatusAsync(Guid id, UpdateServiceStatusRequestDto dto, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
