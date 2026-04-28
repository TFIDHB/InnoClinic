using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppointmentDbContext _context;
        public IAppointmentRepository AppointmentRepository { get; }

        public UnitOfWork(AppointmentDbContext context)
        {
            _context = context;
            AppointmentRepository = new AppointmentRepository(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
