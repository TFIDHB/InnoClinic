using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PatientProfileConfiguration : IEntityTypeConfiguration<PatientProfile>
    {
        public void Configure(EntityTypeBuilder<PatientProfile> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasDefaultValueSql("newsequentialid()").ValueGeneratedOnAdd();

            builder.Property(e => e.FirstName).IsRequired();
            builder.Property(e => e.LastName).IsRequired();
            builder.Property(e => e.MiddleName);
            builder.Property(e => e.IsLinkedToAccount).IsRequired();
        }
    }
}
