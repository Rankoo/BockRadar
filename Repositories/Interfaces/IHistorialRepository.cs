using BookRadar.Models;

namespace BookRadar.Repositories.Interfaces
{
    public interface IHistorialRepository
    {
        Task GuardarBusquedaAsync(BookResultDto book);
        Task<List<BookResultDto>> ObtenerHistorialAsync();
    }
}
