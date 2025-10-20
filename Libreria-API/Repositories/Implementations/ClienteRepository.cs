using Libreria_API.Models;
using Libreria_API.Repositories.Interfaces;
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

        public Cliente? ObtenerPorUsuario(string usuario)
            => _context.Clientes.FirstOrDefault(c => c.Usuario == usuario);

        public void Agregar(Cliente cliente)
            => _context.Clientes.Add(cliente);

        public bool ExisteUsuario(string usuario)
            => _context.Clientes.Any(c => c.Usuario == usuario);

        public async Task GuardarCambiosAsync()
            => await _context.SaveChangesAsync();
    }
}
