using Microsoft.EntityFrameworkCore;
using SlabIngestionService.Models;

namespace SlabIngestionService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Slab> Slabs => Set<Slab>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Slab>(entity =>
            {
                entity.HasKey(s => s.SlabId);

                entity.Property(s => s.Status)
                      .HasConversion<string>();

                entity.Property(s => s.Weight)
                      .HasPrecision(18, 2);

                entity.Property(s => s.Length)
                      .HasPrecision(18, 2);

                entity.Property(s => s.Width)
                      .HasPrecision(18, 2);
            });
        }
    }
}
