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

        modelBuilder.Entity<NotaTrilhaEntity>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("ID_NOTA")
                .ValueGeneratedOnAdd();
        });
    }

    public DbSet<TrilhaEntity> Trilha { get; set; }
    public DbSet<NotaTrilhaEntity> Nota { get; set; }
    public DbSet<UsuarioEntity> Usuario { get; set; }
}
