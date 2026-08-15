using Domain.Entities;
using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IResultRepository : IRepository<Result, Guid>
    {
        Task<Result?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken ct = default);
    }
}
