using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class AppointmentUnitOfWork(AppointmentDbContext context, IAppointmentRepository appointments)
        : IAppointmentUnitOfWork, IDisposable
    {
        public IAppointmentRepository AppointmentRepository => appointments;

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await context.SaveChangesAsync(ct);

        public void Dispose() => context.Dispose();
    }
}
