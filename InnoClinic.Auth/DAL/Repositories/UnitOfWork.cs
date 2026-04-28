using DAL.Interfaces;

namespace DAL.Repositories
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
