using BockRadar.Models;
using BookRadar.Data;
using BookRadar.Models;
using Microsoft.EntityFrameworkCore;

namespace BockRadar.Repositories
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
            var entity = new HistorialBusqueda
            {
                Autor = book.Autor,
                Titulo = book.Title,
                AnioPublicacion = book.Year,
                Editorial = book.Publisher,
                FechaConsulta = book.FechaConsulta
            };

            _context.HistorialBusquedas.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BookResultDto>> ObtenerHistorialAsync()
        {
            return await _context.HistorialBusquedas
                .Select(h => new BookResultDto
                {
                    Autor = h.Autor,
                    Title = h.Titulo,
                    Year = h.AnioPublicacion,
                    Publisher = h.Editorial,
                    FechaConsulta = h.FechaConsulta
                })
                .ToListAsync();
        }
    }
}
