using Microsoft.EntityFrameworkCore;
using ProgressTrackingService.Models;

namespace ProgressTrackingService.Data
{
    public class ProgressDbContext : DbContext
    {
        public DbSet<UserStatistics> UserStatistics { get; set; }
        public DbSet<WorkoutLog> WorkoutLogs { get; set; }
        public DbSet<WeightEntry> WeightEntries { get; set; }

    public ProgressDbContext(DbContextOptions<ProgressDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // UserStatistics
            modelBuilder.Entity<UserStatistics>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.HasIndex(s => s.UserId).IsUnique();

                entity.Property(s => s.CurrentWeight)
                      .HasPrecision(18, 2);
                entity.Property(s => s.StartingWeight)
                      .HasPrecision(18, 2);
            });

            // WorkoutLog
            modelBuilder.Entity<WorkoutLog>(entity =>
            {
                entity.HasKey(w => w.Id);
                entity.HasIndex(w => w.UserId);
            });

            // WeightEntry
            modelBuilder.Entity<WeightEntry>(entity =>
            {
                entity.HasKey(w => w.Id);
                entity.HasIndex(w => w.UserId);

                entity.Property(w => w.WeightKg)
                      .HasPrecision(18, 2);
            });

            base.OnModelCreating(modelBuilder);
        }
    }

}
