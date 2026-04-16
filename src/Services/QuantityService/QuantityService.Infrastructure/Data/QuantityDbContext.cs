using Microsoft.EntityFrameworkCore;
using QuantityService.Core.Entities;

namespace QuantityService.Infrastructure.Data
{
    public class QuantityDbContext : DbContext
    {
        public QuantityDbContext(DbContextOptions<QuantityDbContext> options) 
            : base(options) { }

        public DbSet<Quantity> Quantities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Quantity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Unit).IsRequired().HasMaxLength(50);
                entity.Property(e => e.UnitType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.BaseValue).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
            });
        }
    }
}