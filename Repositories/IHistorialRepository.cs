using BookRadar.Models;

namespace BookRadar.Repositories
{
    public interface IHistorialRepository
    {
        Task GuardarBusquedaAsync(BookResultDto book);
        Task<List<BookResultDto>> ObtenerHistorialAsync();
    }
}
