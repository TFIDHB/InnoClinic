namespace Application.Interfaces
{
    public interface IServicesClient
    {
        Task<int> GetTimeSlotSizeAsync(Guid serviceId, CancellationToken ct = default);
    }
}
