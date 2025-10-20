using Libreria_API.Models;
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
            => await _context.Clientes.FirstOrDefaultAsync(c => c.Usuario == usuario);

        public void Agregar(Cliente cliente)
            => _context.Clientes.Add(cliente);

        public async Task<bool> ExisteUsuarioAsync(string usuario)
            => await _context.Clientes.AnyAsync(c => c.Usuario == usuario);

        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
    }
}
