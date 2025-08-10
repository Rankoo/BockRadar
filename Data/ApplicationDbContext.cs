using BookRadar.Models;
using Microsoft.EntityFrameworkCore;

namespace BookRadar.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tu tabla de historial de búsquedas
        public DbSet<HistorialBusqueda> HistorialBusquedas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la tabla
            modelBuilder.Entity<HistorialBusqueda>(entity =>
            {
                entity.ToTable("HistorialBusquedas");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Autor)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Titulo)
                      .IsRequired()
                      .HasMaxLength(300);

                entity.Property(e => e.AnioPublicacion)
                      .IsRequired(false);

                entity.Property(e => e.Editorial)
                      .HasMaxLength(200);

                entity.Property(e => e.FechaConsulta)
                      .IsRequired();
            });
        }
    }
}