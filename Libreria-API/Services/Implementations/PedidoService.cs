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
    }
}
