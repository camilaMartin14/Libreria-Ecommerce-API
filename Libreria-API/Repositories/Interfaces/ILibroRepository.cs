using Libreria_API.DTOs;
using Libreria_API.Models;

namespace Libreria_API.Repositories.Interfaces
{
    public interface ILibroRepository
    {
        List<LibroDTO> GetLibrosByFilters(string? titulo, string? autor, string? categoria, string? idioma, string? genero);
        Libro? GetLibroById(int id);
    }
}
