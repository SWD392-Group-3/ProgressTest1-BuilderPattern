using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Computer> Computers { get; set; } = null!;
        public DbSet<Laptop> Laptops { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Fix warning: No store type was specified for the decimal property
            modelBuilder.Entity<Computer>()
                .Property(c => c.EstimatedPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Laptop>()
                .Property(l => l.EstimatedPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}
