using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppointmentDbContext : DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : base(options)
        {

        }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Date).HasColumnType("date");
                entity.Property(e => e.Time).HasColumnType("time");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
