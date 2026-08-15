using Application.DTOs;

namespace Application.Interfaces
{
    public interface IServicesClient
    {
        Task<int> GetTimeSlotSizeAsync(Guid serviceId, CancellationToken ct = default);
        Task<string?> GetServiceNameAsync(Guid serviceId, CancellationToken ct = default);
        Task<string?> GetSpecializationNameAsync(Guid specializationId, CancellationToken ct = default);
    }
}
