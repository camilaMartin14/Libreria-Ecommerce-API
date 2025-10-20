using Libreria_API.DTOs;
using Libreria_API.Models;
using Libreria_API.Repositories.Interfaces;
using Libreria_API.Services.Interfaces;

namespace Libreria_API.Services.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repo;

        public ClienteService(IClienteRepository repo)
        {
            _repo = repo;
        }

        public async Task RegistrarAsync(ClienteDTO dto)
        {
            //if (_repo.ExisteUsuario(dto.Usuario))
            //    throw new Exception("El usuario ya existe.");

            //var hash = BCrypt.Net.BCrypt.HashPassword(dto.Contraseña);
            //var cliente = new Cliente
            //{
            //    Nombre = dto.Nombre,
            //    Apellido = dto.Apellido,
            //    Email = dto.Email,
            //    Usuario = dto.Usuario,
            //    Contraseña = hash
            //};

            //_repo.Agregar(cliente);
            //await _repo.GuardarCambiosAsync();
        }

        public Cliente? Login(string usuario, string contraseña)
        {
            //var cliente = _repo.ObtenerPorUsuario(usuario);
            //if (cliente == null) return null;

            //bool valido = BCrypt.Net.BCrypt.Verify(contraseña, cliente.ContraseñaHash);
            //return valido ? cliente : null;
        }
    }

}

