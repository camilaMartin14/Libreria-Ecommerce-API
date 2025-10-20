using Libreria_API.Models;

namespace Libreria_API.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        void Create(Pedido pedido);
        bool UpdateStatus(int id, string estado);
        List<Pedido> GetAll(DateTime? fecha, int? codigoCliente);
        Pedido? GetPedidoById(int id);

    }
}
