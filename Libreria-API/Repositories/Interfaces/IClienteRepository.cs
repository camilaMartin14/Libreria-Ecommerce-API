using Libreria_API.Models;

namespace Libreria_API.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        public interface IClienteRepository
        {
            Cliente? ObtenerPorUsuario(string usuario);
            void Agregar(Cliente cliente);
            bool ExisteUsuario(string usuario);
            Task GuardarCambiosAsync();
        }

    }
}
