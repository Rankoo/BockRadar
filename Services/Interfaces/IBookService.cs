using BookRadar.Models;

namespace BookRadar.Services.Interfaces
{
    public interface IBookService
    {
        Task<List<BookResultDto>> SearchByAuthorAsync(string author);
    }
}