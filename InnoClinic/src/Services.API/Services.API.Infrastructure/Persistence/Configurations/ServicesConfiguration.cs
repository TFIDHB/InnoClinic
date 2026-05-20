using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ServicesConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasDefaultValueSql("newsequentialid()").ValueGeneratedOnAdd();

            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Price).IsRequired();
            builder.Property(e => e.ServiceCategory).IsRequired();
            builder.Property(e => e.Status).IsRequired().HasConversion<string>();
        }
    }
}
