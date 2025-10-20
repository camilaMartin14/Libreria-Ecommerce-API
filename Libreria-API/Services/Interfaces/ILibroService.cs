using Libreria_API.DTOs;

namespace Libreria_API.Services.Interfaces
{
    public interface ILibroService
    {
        List<LibroDTO> GetLibrosByFilters(string? titulo, string? autor, string? categoria, string? idioma, string? genero);

    }
}
