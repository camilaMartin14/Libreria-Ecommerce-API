using Libreria_API.Models;
using Libreria_API.Repositories.Interfaces;

namespace Libreria_API.Repositories.Implementations
{
    public class LibroRepository : ILibroRepository
    {
        private readonly LibreriaContext _context;
        public LibroRepository(LibreriaContext context)
        {
            _context = context;
        }
        public List<Libro> GetAllLibros()
        {
            throw new NotImplementedException();
        }

        public Libro? GetLibroById(int id)
        {
            
        }
    }
}
