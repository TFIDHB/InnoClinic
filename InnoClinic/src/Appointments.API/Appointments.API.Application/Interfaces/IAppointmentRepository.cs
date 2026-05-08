using Domain.Entities;
using InnoClinic.Shared.Interfaces;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment, Guid>
    {
        Task<bool> AnyAsync(Expression<Func<Appointment, bool>> predicate, CancellationToken ct = default);
    }
}
