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
            // Buscamos al cliente según el nombre de usuario
            var cliente = await _repo.ObtenerPorUsuarioAsync(usuario);

            // Si no se encontró ningún cliente con ese usuario, devolvemos null (login inválido)
            if (cliente == null) return null;

            // Verificamos la contraseña
            //    El método VerifyHashedPassword compara el hash guardado con la contraseña ingresada.
            //    Devuelve 'Success' si la contraseña es correcta.
            var resultado = _hasher.VerifyHashedPassword(
                cliente, // contexto (el objeto que contiene la contraseña hasheada)
                cliente.IdUsuarioNavigation.ContrasenaHash, // hash guardado en la BD
                contraseña // contraseña ingresada por el usuario
            );

            // Si la verificación fue exitosa, devolvemos el cliente;
            //     si no, devolvemos null (login fallido).
            return resultado == PasswordVerificationResult.Success ? cliente : null;
        }


        public async Task RegistrarAsync(Cliente c, Usuario u)
        {
            //Validar que el nombre de usuario no exista
            if (await _repo.ExisteUsuarioAsync(u.NombreUsuario))
                throw new Exception("El nombre de usuario ya existe.");

            // Crear un hasher genérico sin dependencia de la entidad Usuario
            var hasher = new PasswordHasher<object>();

            // Crear el objeto Usuario
            var usuario = new Usuario
            {
                NombreUsuario = u.NombreUsuario,
                ContrasenaHash = hasher.HashPassword(null, u.ContrasenaHash),
                Rol = "Cliente",
                FechaAlta = DateTime.Now
            };

            // Crear el objeto Cliente con los datos personales
            var cliente = new Cliente
            {
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                NroDoc = c.NroDoc,
                IdTipoDoc = c.IdTipoDoc,
                IdSexo = c.IdSexo,
                IdNacionalidad = c.IdNacionalidad,
                FechaRegistro = DateTime.Now,
                FechaNacimiento = c.FechaNacimiento,
                IdBarrio = c.IdBarrio,
                Calle = c.Calle,
                Nro = c.Nro,
                Piso = c.Piso,
                Dpto = c.Dpto,
                Cp = c.Cp,
                Email = c.Email
            };

            // Guardar usuario y cliente (el repo maneja ambos)
            await _repo.AgregarClienteConUsuarioAsync(cliente, usuario);
        }
    }
}


