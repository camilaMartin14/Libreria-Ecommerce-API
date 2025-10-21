using Libreria_API.DTOs;
using Libreria_API.Models;
using Libreria_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Libreria_API.Repositories.Implementations
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly LibreriaContext _context;
        public PedidoRepository(LibreriaContext context) => _context = context;

        public void Create(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            _context.SaveChanges(); // Genera NroPedido

            var primerTracking = new TrackingEnvio
            {
                NroPedido = pedido.NroPedido,
                IdEstadoEnvio = 1, // Pendiente
                FechaEstado = DateTime.Now,
                Observaciones = "Pedido creado"
            };

            _context.TrackingEnvios.Add(primerTracking);
            _context.SaveChanges();
        }

        public List<PedidoDTO> GetAll(DateTime? fecha, int? codigoCliente)
        {
            var query = _context.Pedidos
                .Include(p => p.CodClienteNavigation)
                .Include(p => p.IdFormaEnvioNavigation)
                .Include(p => p.DetallePedidos)
                    .ThenInclude(d => d.CodLibroNavigation)
                .Include(p => p.TrackingEnvios)
                    .ThenInclude(t => t.IdEstadoEnvioNavigation)
                .AsQueryable();

            if (fecha.HasValue)
                query = query.Where(p => p.Fecha >= fecha.Value.Date && p.Fecha < fecha.Value.Date.AddDays(1));

            if (codigoCliente.HasValue)
                query = query.Where(p => p.CodCliente == codigoCliente.Value);

            return query
                .Select(p => new PedidoDTO
                {
                    NroPedido = p.NroPedido,
                    Fecha = p.Fecha,
                    FechaEntrega = p.FechaEntrega,
                    InstruccionesAdicionales = p.InstruccionesAdicionales,
                    CodCliente = p.CodCliente,
                    NombreCliente = p.CodClienteNavigation.Nombre,
                    IdFormaEnvio = p.IdFormaEnvio,
                    NombreFormaEnvio = p.IdFormaEnvioNavigation.FormaEnvio,
                    EstadoActual = p.TrackingEnvios
                        .OrderByDescending(t => t.FechaEstado)
                        .Select(t => t.IdEstadoEnvioNavigation.EstadoActual)
                        .FirstOrDefault() ?? "Sin estado",
                    Detalles = p.DetallePedidos.Select(d => new DetalleDTO
                    {
                        IdDetallePedido = d.IdDetallePedido,
                        CodLibro = d.CodLibro,
                        TituloLibro = d.CodLibroNavigation.Titulo,
                        Cantidad = d.Cantidad,
                        Precio = d.Precio
                    }).ToList()
                })
                .ToList();
        }

        public Pedido? GetPedidoById(int id)
        {
            return _context.Pedidos
                .Include(p => p.CodClienteNavigation)
                .Include(p => p.IdFormaEnvioNavigation)
                .Include(p => p.DetallePedidos)
                    .ThenInclude(d => d.CodLibroNavigation)
                .Include(p => p.TrackingEnvios)
                    .ThenInclude(t => t.IdEstadoEnvioNavigation)
                .FirstOrDefault(p => p.NroPedido == id);
        }

        public void UpdateStatus(int nroPedido, int nuevoEstadoId, string observaciones)
        {
            var pedido = _context.Pedidos
                .Include(p => p.TrackingEnvios)
                .FirstOrDefault(p => p.NroPedido == nroPedido);

            if (pedido == null)
                throw new KeyNotFoundException("Pedido no encontrado");

            var tracking = new TrackingEnvio
            {
                NroPedido = nroPedido,
                IdEstadoEnvio = nuevoEstadoId,
                FechaEstado = DateTime.Now,
                Observaciones = observaciones
            };

            _context.TrackingEnvios.Add(tracking);
            _context.SaveChanges();
        }

        public string ObtenerEstadoActualPedido(int nroPedido)
        {
            var estado = _context.TrackingEnvios
                .Where(t => t.NroPedido == nroPedido)
                .OrderByDescending(t => t.FechaEstado)
                .Select(t => t.IdEstadoEnvioNavigation.EstadoActual)
                .FirstOrDefault();

            return estado ?? "Sin estado";
        }
    }
}
