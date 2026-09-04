using InnoClinic.Documents.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Documents.API.Infrastructure.Persistance
{
    public class DocumentsDbContext : DbContext
    {
        public DocumentsDbContext(DbContextOptions<DocumentsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocumentsDbContext).Assembly);
        }
    }
}
