using Libreria_API.DTOs;
using Libreria_API.Models;

namespace Libreria_API.Services.Interfaces
{
    public interface ILibroService
    {
        List<LibroDTO> GetLibrosByFilters(string? titulo, string? autor, string? categoria, string? idioma, string? genero);

    }
}
