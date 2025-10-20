using Libreria_API.Models;
using Libreria_API.Repositories.Interfaces;

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
            throw new NotImplementedException();
        }

        public List<Pedido> GetAll(DateTime? fecha, int? codigoCliente)
        {
            throw new NotImplementedException();
        }

        public Pedido? GetPedidoById(int id)
        {
            throw new NotImplementedException();
        }

        public bool SoftDelete(int id, string estado)
        {
            throw new NotImplementedException();
        }

        public void Update(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStatus(int id, string estado)
        {
            throw new NotImplementedException();
        }
    }
}
