using Libreria_API.Models;

namespace Libreria_API.Services.Interfaces
{
    public interface ILibroService
    {
        List<Libro> GetLibrosByFilters(string autor, string categoria, string idioma, string genero);

    }
}
