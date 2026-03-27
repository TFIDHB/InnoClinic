using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Patterns
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthDbContext _authDbContext;
        public IUserRepository userRepository;

        public UnitOfWork(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _authDbContext.SaveChangesAsync();
        }
    }
}
