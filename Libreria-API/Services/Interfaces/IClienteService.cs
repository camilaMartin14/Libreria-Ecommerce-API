using Libreria_API.DTOs;
using Libreria_API.Models;

namespace Libreria_API.Services.Interfaces
{
    public interface IClienteService
    {
            Task RegistrarAsync(ClienteDTO dto);
            Cliente? Login(string usuario, string contraseña);
    }
}
