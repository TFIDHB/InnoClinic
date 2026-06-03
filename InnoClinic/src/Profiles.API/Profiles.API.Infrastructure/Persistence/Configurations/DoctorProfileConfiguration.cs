using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DoctorProfileConfiguration : IEntityTypeConfiguration<DoctorProfile>
    {
        public void Configure(EntityTypeBuilder<DoctorProfile> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasDefaultValueSql("newsequentialid()").ValueGeneratedOnAdd();

            builder.Property(e => e.FirstName).IsRequired();
            builder.Property(e => e.LastName).IsRequired();
            builder.Property(e => e.MiddleName);
            builder.Property(e => e.CareerStartYear).IsRequired();
            builder.Property(e => e.Status).HasConversion<string>();

            builder.HasOne(e => e.Specialization).WithMany().HasForeignKey(e => e.SpecializationId);
        }
    }
}
