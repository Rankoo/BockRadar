namespace BookRadar.Models
{
    // Esquema de devolución de libros
    public class BookResultDto
    {
        public string Title { get; set; } = string.Empty;
        public int? Year { get; set; }
        public string? Publisher { get; set; }
        public string Autor { get; set; } = string.Empty;
        public DateTime FechaConsulta { get; set; }
    }
}