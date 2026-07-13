using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Repositories
{
    public class AppointmentUnitOfWork(AppointmentDbContext context, IServiceProvider provider)
        : IAppointmentUnitOfWork, IDisposable
    {
        private IAppointmentRepository? _appointmentRepository;
        public IAppointmentRepository AppointmentRepository =>
            _appointmentRepository ??= provider.GetRequiredService<IAppointmentRepository>();
        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await context.SaveChangesAsync(ct);
        public void Dispose() => context.Dispose();
    }
}
