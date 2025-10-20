using Libreria_API.Models;

namespace Libreria_API.DTOs
{
    public class LibroDTO
    {
        public string Isbn { get; set; }

        public string Titulo { get; set; }

        public decimal Precio { get; set; }

        public string Descripcion { get; set; }

        public int Stock { get; set; }

        public int IdIdioma { get; set; }

        public virtual ICollection<AutoresLibro> AutoresLibros { get; set; } = new List<AutoresLibro>();

        public virtual ICollection<LibrosCategoria> LibrosCategoria { get; set; } = new List<LibrosCategoria>();

        public virtual ICollection<LibrosGenero> LibrosGeneros { get; set; } = new List<LibrosGenero>();
    }
}
