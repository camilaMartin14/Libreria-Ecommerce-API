using Libreria_API.Models;

namespace Libreria_API.DTOs
{
    public class PedidoDTO
    {
        public int NroPedido { get; set; }

        public DateTime Fecha { get; set; }

        public DateOnly FechaEntrega { get; set; }

        public string InstruccionesAdicionales { get; set; }

        public int CodCliente { get; set; }

        public int IdFormaEnvio { get; set; }

        public virtual Cliente CodClienteNavigation { get; set; }

        public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

        public virtual FormasEnvio IdFormaEnvioNavigation { get; set; }

        public virtual ICollection<TrackingEnvio> TrackingEnvios { get; set; } = new List<TrackingEnvio>();
    }
}
