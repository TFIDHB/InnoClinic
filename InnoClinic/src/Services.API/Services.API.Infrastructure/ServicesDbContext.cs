using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ServicesDbContext : DbContext
    {
        public ServicesDbContext(DbContextOptions<ServicesDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServicesDbContext).Assembly);
        }
    }
}
