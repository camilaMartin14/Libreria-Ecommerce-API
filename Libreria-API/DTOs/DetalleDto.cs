using Libreria_API.Models;

namespace Libreria_API.DTOs
{
    public class DetalleDTO
    {
        public int IdDetallePedido { get; set; }
        public int CodLibro { get; set; }
        public string TituloLibro { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
