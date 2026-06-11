using Domain.Entities;
using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface ISpecializationsRepository : IRepository<Specialization, Guid>
    {
        Task<Specialization?> GetByIdWithServicesAsync(Guid id, CancellationToken ct = default);
    }
}
