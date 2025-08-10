using BookRadar.Models;

namespace BockRadar.Repositories
{
    public interface IHistorialRepository
    {
        Task GuardarBusquedaAsync(BookResultDto book);
        Task<List<BookResultDto>> ObtenerHistorialAsync();
    }
}
