namespace BookRadar.Models
{
    public class BookSearchViewModel
    {
        public List<BookResultDto> SearchResults { get; set; } = new();
        public List<BookResultDto> Historial { get; set; } = new();
    }
}