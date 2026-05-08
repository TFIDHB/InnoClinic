using Application.Interfaces;
using Domain.Entities;
using InnoClinic.Shared.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class AppointmentRepository(AppointmentDbContext context) : BaseRepository<Appointment, Guid>(context), IAppointmentRepository
    {
    }
}
