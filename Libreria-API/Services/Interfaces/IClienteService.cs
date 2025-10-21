using Libreria_API.DTOs;
using Libreria_API.Models;

namespace Libreria_API.Services.Interfaces
{
    public interface IClienteService
    {
        Task<Cliente?> LoginAsync(string usuario, string contraseña);
        Task RegistrarAsync(Cliente c, Usuario u);
    }
}
