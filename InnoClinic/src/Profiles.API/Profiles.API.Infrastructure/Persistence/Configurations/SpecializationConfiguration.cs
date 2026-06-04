using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        private static readonly Guid TherapistId = Guid.Parse("a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1");
        private static readonly Guid SurgeonId = Guid.Parse("a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2");
        private static readonly Guid OphthalmologistId = Guid.Parse("a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3");

        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasDefaultValueSql("newsequentialid()").ValueGeneratedOnAdd();
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.IsActive).IsRequired();

            builder.HasData(
                new Specialization { Id = TherapistId, Name = "Therapist", IsActive = true },
                new Specialization { Id = SurgeonId, Name = "Surgeon", IsActive = true },
                new Specialization { Id = OphthalmologistId, Name = "Ophthalmologist", IsActive = true });
        }
    }
}
