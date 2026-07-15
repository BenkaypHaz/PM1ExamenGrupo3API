using Microsoft.EntityFrameworkCore;
using Ubicaciones.Dominio.Entidades;

namespace Ubicaciones.Infraestructura.Persistencia;

public class ContextoBd : DbContext
{
    public ContextoBd(DbContextOptions<ContextoBd> opciones) : base(opciones)
    {
    }

    public DbSet<Contacto> Contactos => Set<Contacto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contacto>(entidad =>
        {
            entidad.ToTable("contactos");
            entidad.HasKey(c => c.Id);
            entidad.Property(c => c.Nombre).HasMaxLength(100).IsRequired();
            entidad.Property(c => c.Telefono).HasMaxLength(8).IsRequired();
            entidad.Property(c => c.Latitud).IsRequired();
            entidad.Property(c => c.Longitud).IsRequired();
            entidad.Property(c => c.Imagen).HasColumnType("LONGBLOB");
            entidad.Property(c => c.VideoRuta).HasMaxLength(500);
        });
    }
}
