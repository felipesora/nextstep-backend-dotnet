using Microsoft.EntityFrameworkCore;
using NS.Domain.Entities;

namespace NS.Infra.Data.AppData;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrilhaEntity>(entity =>
        {
            entity.Property(e => e.Area).HasConversion<string>();
            entity.Property(e => e.Nivel).HasConversion<string>();
            entity.Property(e => e.Status).HasConversion<string>();
        });
    }

    public DbSet<TrilhaEntity> Trilha { get; set; }
}
