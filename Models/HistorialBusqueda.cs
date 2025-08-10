using System.ComponentModel.DataAnnotations;

namespace BockRadar.Models
{
    public class HistorialBusqueda
    {
        public int Id { get; set; }
        public string Autor { get; set; } = string.Empty;
        public string Titulo{ get; set; } = string.Empty;
        public int? AnioPublicacion { get; set; }
        public string? Editorial { get; set; }
        public DateTime FechaConsulta { get; set; }
    }
}
