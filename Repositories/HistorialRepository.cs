using BookRadar.Models;
using BookRadar.Data;
using Microsoft.EntityFrameworkCore;

namespace BookRadar.Repositories
{
    public class HistorialRepository : IHistorialRepository
    {
        private readonly ApplicationDbContext _context;

        public HistorialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task GuardarBusquedaAsync(BookResultDto book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            // Ejecuta SP para insertar
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $@"EXEC sp_GuardarBusqueda 
                    {book.Autor}, 
                    {book.Title}, 
                    {book.Year}, 
                    {book.Publisher}, 
                    {book.FechaConsulta}"
            );
        }

        public async Task<List<BookResultDto>> ObtenerHistorialAsync()
        {
            var historial = await _context.HistorialBusquedas
                .FromSqlInterpolated($"EXEC sp_ObtenerHistorial")
                .ToListAsync();

            // Ordenar por año de publicación descendente, luego por fecha de consulta
            return historial.Select(h => new BookResultDto
            {
                Autor = h.Autor,
                Title = h.Titulo,
                Year = h.AnioPublicacion,
                Publisher = h.Editorial,
                FechaConsulta = h.FechaConsulta
            })
            .OrderByDescending(b => b.Year)
            .ThenByDescending(b => b.FechaConsulta)
            .ToList();
        }
    }
}
