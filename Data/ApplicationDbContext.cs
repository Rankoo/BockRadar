using BockRadar.Models;
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
    }
}