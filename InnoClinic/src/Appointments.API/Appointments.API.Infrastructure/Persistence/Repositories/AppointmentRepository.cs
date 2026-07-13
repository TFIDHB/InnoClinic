using Application.Interfaces;
using InnoClinic.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class AppointmentRepository(AppointmentDbContext context) : BaseRepository<Appointment, Guid>(context), IAppointmentRepository
    {
        public async Task<IEnumerable<Appointment>> GetByDateAndDoctorAsync(
            DateOnly date,
            Guid? doctorId,
            CancellationToken ct = default)
        {
            return await context.Appointments
                .Where(a => a.Date == date && (doctorId == null || a.DoctorId == doctorId))
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<Appointment>> GetByDateRangeAndDoctorAsync(
            DateOnly startDate,
            DateOnly endDate,
            Guid? doctorId,
            CancellationToken ct = default)
        {
            return await context.Appointments
                .Where(a => a.Date <= startDate && a.Date >= endDate && (doctorId == null || a.DoctorId == doctorId))
                .ToListAsync(ct);
        }
    }
}
