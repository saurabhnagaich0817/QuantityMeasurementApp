using Microsoft.EntityFrameworkCore;
using HistoryService.Core.Entities;

namespace HistoryService.Infrastructure.Data
{
    public class HistoryDbContext : DbContext
    {
        public HistoryDbContext(DbContextOptions<HistoryDbContext> options) 
            : base(options) { }

        public DbSet<HistoryEntry> HistoryEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HistoryEntry>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.CreatedAt);
                
                entity.Property(e => e.OperationType)
                    .IsRequired()
                    .HasMaxLength(50);
                    
                entity.Property(e => e.InputValues)
                    .IsRequired()
                    .HasMaxLength(500);
                    
                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasMaxLength(200);
                    
                entity.Property(e => e.CreatedAt)
                    .IsRequired();
            });
        }
    }
}