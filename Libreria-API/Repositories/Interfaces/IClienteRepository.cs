using Libreria_API.Models;

namespace Libreria_API.Repositories.Interfaces
{
        public interface IClienteRepository
        {
            Task<Cliente?> ObtenerPorUsuarioAsync(string usuario);
            void Agregar(Cliente cliente);
            Task<bool> ExisteUsuarioAsync(string usuario);
            Task GuardarCambiosAsync();
        }
    }
