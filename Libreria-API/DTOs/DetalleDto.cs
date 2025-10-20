using Libreria_API.Models;

namespace Libreria_API.DTOs
{
    public class DetalleDto
    {

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public int CodLibro { get; set; }
    }
}
