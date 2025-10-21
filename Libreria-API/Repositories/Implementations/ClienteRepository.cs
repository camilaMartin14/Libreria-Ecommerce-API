﻿using Libreria_API.Models;
using Libreria_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Libreria_API.Repositories.Implementations
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly LibreriaContext _context;

        public ClienteRepository(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<Cliente?> ObtenerPorUsuarioAsync(string usuario)
            => await _context.Clientes.Include(c => c.IdUsuarioNavigation)
            .FirstOrDefaultAsync(c => c.IdUsuarioNavigation.NombreUsuario == usuario);


        public async Task<Cliente> AgregarClienteConUsuarioAsync(Cliente cliente, Usuario usuario)
        {
            // Agrego el usuario
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync(); // Guardo

            //Asocio el cliente al usuario recién creado
            cliente.IdUsuario = usuario.IdUsuario;
            _context.Clientes.Add(cliente);

            await _context.SaveChangesAsync(); // Guardo

            return cliente;
        }


        public async Task<bool> ExisteUsuarioAsync(string nombreUsuario)
        => await _context.Usuarios.AnyAsync(u => u.NombreUsuario == nombreUsuario);


        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
    }
}
