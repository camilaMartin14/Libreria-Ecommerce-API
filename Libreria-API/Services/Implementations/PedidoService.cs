using Libreria_API.DTOs;
using Libreria_API.Models;
using Libreria_API.Repositories.Interfaces;
using Libreria_API.Services.Interfaces;

namespace Libreria_API.Services.Implementations
{
    public class PedidoService: IPedidoService
    {
        private readonly IPedidoRepository _repo;
        public PedidoService(IPedidoRepository repo) => _repo = repo;

        public void Create(Pedido pedido) => _repo.Create(pedido);

        public List<PedidoDTO> GetAll(DateTime? fecha, int? codigoCliente)
            => _repo.GetAll(fecha, codigoCliente);

        public PedidoDTO? GetPedidoById(int id)
        {
            var pedido = _repo.GetPedidoById(id);
            if (pedido == null) return null;

            return new PedidoDTO
            {
                NroPedido = pedido.NroPedido,
                Fecha = pedido.Fecha,
                FechaEntrega = pedido.FechaEntrega,
                InstruccionesAdicionales = pedido.InstruccionesAdicionales,
                CodCliente = pedido.CodCliente,
                NombreCliente = pedido.CodClienteNavigation?.Nombre,
                IdFormaEnvio = pedido.IdFormaEnvio,
                NombreFormaEnvio = pedido.IdFormaEnvioNavigation?.FormaEnvio,
                EstadoActual = pedido.TrackingEnvios
                                .OrderByDescending(t => t.FechaEstado)
                                .Select(t => t.IdEstadoEnvioNavigation.EstadoActual)
                                .FirstOrDefault() ?? "Sin estado",
                Detalles = pedido.DetallePedidos.Select(d => new DetalleDTO
                {
                    IdDetallePedido = d.IdDetallePedido,
                    CodLibro = d.CodLibro,
                    TituloLibro = d.CodLibroNavigation?.Titulo,
                    Cantidad = d.Cantidad,
                    Precio = d.Precio
                }).ToList()
            };
        }

        public string ObtenerEstadoActualPedido(int nroPedido)
            => _repo.ObtenerEstadoActualPedido(nroPedido);

        public void UpdateStatus(int nroPedido, int nuevoEstadoId, string observaciones)
            => _repo.UpdateStatus(nroPedido, nuevoEstadoId, observaciones);
    }
}
