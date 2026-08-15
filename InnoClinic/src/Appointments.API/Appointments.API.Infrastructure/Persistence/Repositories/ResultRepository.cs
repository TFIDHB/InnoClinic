using Application.Interfaces;
using Domain.Entities;
using InnoClinic.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ResultRepository(AppointmentDbContext context) : BaseRepository<Result, Guid>(context), IResultRepository
    {
        public async Task<Result?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken ct = default)
        {
            return await DbSet.FirstOrDefaultAsync(e => e.AppointmentId == appointmentId, ct);
        }
    }
}
