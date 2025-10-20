using Libreria_API.Models;
using Libreria_API.Repositories.Interfaces;
using Libreria_API.Services.Interfaces;

namespace Libreria_API.Services.Implementations
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository _repo;
        public LibroService(ILibroRepository repo)
        {
            _repo = repo;
        }
        public List<Libro> GetLibrosByFilters(string autor, string categoria, string idioma, string genero)
        {
            throw new NotImplementedException();
        }
    }
}
