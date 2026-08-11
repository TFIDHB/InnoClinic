using Domain.Entities;
using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IReceptionistProfilesRepository : IRepository<ReceptionistProfile, Guid>
    {
        Task<ReceptionistProfile?> GetByAccountIdAsync(Guid id, CancellationToken ct = default);
    }
}
