using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class AppointmentRepository : BasicRepository<Appointment, Guid>, IAppointmentRepository
    {
        public AppointmentRepository(AppointmentDbContext context) : base(context)
        {
        }
    }
}
