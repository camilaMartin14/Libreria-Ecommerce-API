using Libreria_API.DTOs;
using Libreria_API.Models;
using Libreria_API.Repositories.Interfaces;
using Libreria_API.Services.Interfaces;

namespace Libreria_API.Services.Implementations
{
    public class PedidoService: IPedidoService
    {
        private readonly IPedidoRepository _repo;
        public PedidoService(IPedidoRepository repo)
        {
            _repo = repo;
        }

        public void Create(Pedido pedido)
        {
            _repo.Create(pedido);
        }

        public List<PedidoDTO> GetAll(DateTime? fecha, int? codigoCliente)
        {
            return _repo.GetAll(fecha, codigoCliente);
        }

        public Pedido? GetPedidoById(int id)
        {
            return _repo.GetPedidoById(id);
        }

        public string ObtenerEstadoActualPedido(int nroPedido)
        {
            return _repo.ObtenerEstadoActualPedido(nroPedido);
        }

        public void UpdateStatus(int nroPedido, int nuevoEstadoId, string observaciones)
        {
             _repo.UpdateStatus(nroPedido, nuevoEstadoId, observaciones);
        }
    }
}
