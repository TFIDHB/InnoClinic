using Application.Interfaces;
using Domain.Entities;
using InnoClinic.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class AppointmentRepository(AppointmentDbContext context) : BaseRepository<Appointment, Guid>(context), IAppointmentRepository
    {
        public async Task<bool> AnyAsync(Expression<Func<Appointment, bool>> predicate)
        {
            return await context.Appointments.AnyAsync(predicate);
        }
    }
}
