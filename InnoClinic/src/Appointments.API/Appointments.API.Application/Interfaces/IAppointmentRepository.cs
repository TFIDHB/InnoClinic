using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment, Guid>
    {
        Task<bool> AnyAsync(Expression<Func<Appointment, bool>> predicate);
    }
}
