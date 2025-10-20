using Libreria_API.DTOs;
using Libreria_API.Models;

namespace Libreria_API.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        void Create(Pedido pedido);
        void UpdateStatus(int nroPedido, int nuevoEstadoId, string observaciones);
        List<PedidoDTO> GetAll(DateTime? fecha, int? codigoCliente);
        Pedido? GetPedidoById(int id);
        string ObtenerEstadoActualPedido(int nroPedido);
    }
}
