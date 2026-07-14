using Domain.Entities;
using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment, Guid>
    {
        Task<IEnumerable<Appointment>> GetByDateAndDoctorAsync(
            DateOnly date,
            Guid? doctorId,
            CancellationToken ct = default);

        Task<IEnumerable<Appointment>> GetByDateRangeAndDoctorAsync(
            DateOnly startDate,
            DateOnly endDate,
            Guid? doctorId,
            CancellationToken ct = default);
    }
}
