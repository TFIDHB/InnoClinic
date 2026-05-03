using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.PatientId).IsRequired();
            builder.Property(e => e.DoctorId).IsRequired();
            builder.Property(e => e.ServiceId).IsRequired();
            builder.Property(e => e.OfficeId).IsRequired();

            builder.Property(e => e.Date).HasColumnType("date");
            builder.Property(e => e.Time).HasColumnType("time");

            builder.Property(e => e.Status).HasConversion<string>();

            builder.Property(e => e.CreatedAt).HasDefaultValueSql("now() AT TIME ZONE 'utc'").ValueGeneratedOnAdd();
        }
    }
}
