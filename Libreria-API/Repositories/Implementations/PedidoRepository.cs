using Libreria_API.DTOs;
using Libreria_API.Models;
using Libreria_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Libreria_API.Repositories.Implementations
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly LibreriaContext _context;
        public PedidoRepository(LibreriaContext context)
        {
            _context = context;
        }

        public void Create(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);

            //Agregar el primer tracking (estado inicial)
            var primerEstado = new TrackingEnvio
            {
                NroPedido = pedido.NroPedido,
                IdEstadoEnvio = 1, // Suponiendo que 1 = "Pendiente"
                FechaEstado = DateTime.Now,
                Observaciones = "Pedido creado"
            };

            pedido.TrackingEnvios.Add(primerEstado);

            // 3. Guardar cambios
            _context.SaveChanges();
        }

        public List<PedidoDTO> GetAll(DateTime? fecha, int? codigoCliente)
        {
            var query = _context.Pedidos
                               .Include(p => p.CodClienteNavigation)
                               .Include(p => p.DetallePedidos)
                               .Include(p => p.IdFormaEnvioNavigation)
                               .Include(p => p.TrackingEnvios)
                               .ThenInclude(t => t.IdEstadoEnvioNavigation)
                               .AsQueryable();

            // 2. Aplicar filtros opcionales
            if (fecha.HasValue)
                query = query.Where(p => p.Fecha.Date == fecha.Value.Date);

            if (codigoCliente.HasValue)
                query = query.Where(p => p.CodCliente == codigoCliente.Value);

            // 3. Mapear a DTO
            var listaDTO = query
                .Select(p => new PedidoDTO
                {
                    NroPedido = p.NroPedido,
                    Fecha = p.Fecha,
                    FechaEntrega = p.FechaEntrega,
                    InstruccionesAdicionales = p.InstruccionesAdicionales,
                    CodCliente = p.CodCliente,
                    IdFormaEnvio = p.IdFormaEnvio,
                    CodClienteNavigation = p.CodClienteNavigation,
                    DetallePedidos = p.DetallePedidos,
                    IdFormaEnvioNavigation = p.IdFormaEnvioNavigation,
                    TrackingEnvios = p.TrackingEnvios
                })
                .ToList();

            return listaDTO;
        }

        public Pedido? GetPedidoById(int id)
        {
            var pedido = _context.Pedidos.Find(id);
            return pedido;
        }

        public void UpdateStatus(int nroPedido, int nuevoEstadoId, string observaciones)
        {
            var pedido = _context.Pedidos
                                .Include(p => p.TrackingEnvios)
                                .FirstOrDefault(p => p.NroPedido == nroPedido);
            if (pedido == null)
                throw new Exception("Pedido no encontrado");

            var nuevoTracking = new TrackingEnvio
            {
                NroPedido = nroPedido,
                IdEstadoEnvio = nuevoEstadoId,
                FechaEstado = DateTime.Now,
                Observaciones = observaciones
            };

            pedido.TrackingEnvios.Add(nuevoTracking);
            _context.SaveChanges();

            //cada vez que llamo a CambiarEstadoPedido,
            //se agrega un nuevo TrackingEnvio
        }

        public string ObtenerEstadoActualPedido(int nroPedido)
        {
            var estadoActual = _context.TrackingEnvios
                                .Where(t => t.NroPedido == nroPedido)
                                .OrderByDescending(t => t.FechaEstado)
                                .Select(t => t.IdEstadoEnvioNavigation.EstadoActual)
                                .FirstOrDefault();

            return estadoActual ?? "Sin estado";
        }
    }
}
