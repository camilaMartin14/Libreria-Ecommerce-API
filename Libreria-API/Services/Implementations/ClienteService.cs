using Libreria_API.DTOs;
using Libreria_API.Models;
using Libreria_API.Repositories.Interfaces;
using Libreria_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Libreria_API.Services.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repo;
        private readonly PasswordHasher<Cliente> _hasher;

        public ClienteService(IClienteRepository repo)
        {
            _repo = repo;
            _hasher = new PasswordHasher<Cliente>();
        }

        public async Task<Cliente?> LoginAsync(string usuario, string contraseña)
        {
            var cliente = await _repo.ObtenerPorUsuarioAsync(usuario);
            if (cliente == null) return null;

            var resultado = _hasher.VerifyHashedPassword(cliente, cliente.Contraseña, contraseña);
            return resultado == PasswordVerificationResult.Success ? cliente : null;
        }

        public async Task RegistrarAsync(ClienteDTO dto)
        {
            if (await _repo.ExisteUsuarioAsync(dto.Usuario))
                throw new Exception("El usuario ya existe.");

            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email,
                Usuario = dto.Usuario
            };

            // Generar hash seguro de la contraseña
            cliente.Contraseña = _hasher.HashPassword(cliente, dto.Contraseña);

            _repo.Agregar(cliente);
            await _repo.GuardarCambiosAsync();
        }
    }
}


