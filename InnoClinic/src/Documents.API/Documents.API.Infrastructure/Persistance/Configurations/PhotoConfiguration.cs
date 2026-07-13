using Documents.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Documents.API.Infrastructure.Persistance.Configurations
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasDefaultValueSql("newsequentialid()").ValueGeneratedOnAdd();
            builder.Property(e => e.Url).IsRequired();
            builder.Property(e => e.Type).IsRequired().HasConversion<string>();
        }
    }
}
