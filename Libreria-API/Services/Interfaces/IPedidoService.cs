using Libreria_API.DTOs;
using Libreria_API.Models;

namespace Libreria_API.Services.Interfaces
{
    public interface IPedidoService
    {
        void Create(Pedido pedido);
        void UpdateStatus(int nroPedido, int nuevoEstadoId, string observaciones);
        List<PedidoDTO> GetAll(DateTime? fecha, int? codigoCliente);
        PedidoDTO? GetPedidoById(int id);
        string ObtenerEstadoActualPedido(int nroPedido);
    }
}
