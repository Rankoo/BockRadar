namespace BookRadar.Models
{
    // Esquema de devoluci�n de libros
    public class BookResultDto
    {
        public string Title { get; set; } = string.Empty;
        public int? Year { get; set; }
        public string? Publisher { get; set; }
    }
}