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

        modelBuilder.Entity<ConteudoEntity>(entity =>
        {
            entity.Property(e => e.Tipo).HasConversion<string>();
        });

        modelBuilder.Entity<NotaTrilhaEntity>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("ID_NOTA")
                .ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<FormularioEntity>(entity =>
        {
            entity.Property(e => e.NivelExperiencia).HasConversion<string>();
            entity.Property(e => e.ObjetivoCarreira).HasConversion<string>();
            entity.Property(e => e.AreaTecnologia1).HasConversion<string>();
            entity.Property(e => e.AreaTecnologia2).HasConversion<string>();
            entity.Property(e => e.AreaTecnologia3).HasConversion<string>();
            entity.Property(e => e.HorasEstudo).HasConversion<string>();
        });
    }

    public DbSet<TrilhaEntity> Trilha { get; set; }
    public DbSet<NotaTrilhaEntity> Nota { get; set; }
    public DbSet<UsuarioEntity> Usuario { get; set; }
    public DbSet<ConteudoEntity> Conteudo { get; set; }
    public DbSet<FormularioEntity> Formulario { get; set; }
}
