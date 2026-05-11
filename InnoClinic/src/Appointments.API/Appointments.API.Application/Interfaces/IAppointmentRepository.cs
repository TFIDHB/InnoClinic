using Domain.Entities;
using InnoClinic.Shared.Interfaces;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment, Guid>
    {
        Task<IEnumerable<Appointment>> GetByDateAndDoctorAsync(DateOnly date, Guid? doctorId, CancellationToken ct = default);
        Task<IEnumerable<Appointment>> GetByDateRangeAndDoctorAsync(DateOnly from, DateOnly to, Guid? doctorId, CancellationToken ct = default);
    }
}
