using Libreria_API.Models;
using Libreria_API.DTOs;

namespace Libreria_API.DTOs
{
    public class PedidoDTO
    {
        public int NroPedido { get; set; }
        public DateTime Fecha { get; set; }
        public DateOnly FechaEntrega { get; set; }
        public string InstruccionesAdicionales { get; set; }
        public int CodCliente { get; set; }
        public string NombreCliente { get; set; } // Solo nombre para no exponer toda la entidad
        public int IdFormaEnvio { get; set; }
        public string NombreFormaEnvio { get; set; } // igual
        public string EstadoActual { get; set; }
        public List<DetalleDTO> Detalles { get; set; } = new();
    }
}
