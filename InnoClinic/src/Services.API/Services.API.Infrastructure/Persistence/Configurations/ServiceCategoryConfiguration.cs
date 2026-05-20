using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
    {
        private static readonly Guid AnalysesId = Guid.Parse("a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1");
        private static readonly Guid ConsultationId = Guid.Parse("a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2");
        private static readonly Guid DiagnosticsId = Guid.Parse("a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3");

        public void Configure(EntityTypeBuilder<ServiceCategory> builder) 
        { 
            builder.HasKey(x => x.Id);

            builder.HasData(
                new ServiceCategory { Id = AnalysesId, Name = "Analyses" },
                new ServiceCategory { Id = ConsultationId, Name = "Consultation" },
                new ServiceCategory { Id = DiagnosticsId, Name = "Diagnostics" });
        }
    }
}
