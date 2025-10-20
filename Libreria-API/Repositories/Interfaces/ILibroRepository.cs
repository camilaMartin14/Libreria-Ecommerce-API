using Libreria_API.Models;

namespace Libreria_API.Repositories.Interfaces
{
    public interface ILibroRepository
    {
        List<Libro> GetAllLibros();
        Libro? GetLibroById(int id);
    }
}
