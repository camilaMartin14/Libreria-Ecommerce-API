using Libreria_API.Models;

namespace Libreria_API.DTOs
{
    public class LibroDTO
    {
        public string Titulo { get; set; }
        public string Editorial { get; set; }
        public string Idioma { get; set; }
        public List<string> Autores { get; set; }
        public List<string> Categorias { get; set; }
        public List<string> Generos { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
