using Libreria_API.DTOs;
using Libreria_API.Models;
using Libreria_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;

namespace Libreria_API.Repositories.Implementations
{
    public class LibroRepository : ILibroRepository
    {
        private readonly LibreriaContext _context;
        public LibroRepository(LibreriaContext context)
        {
            _context = context;
        }

        public Libro? GetLibroById(int id)
        {
            var libro = _context.Libros.Find(id);
            return libro;
        }

        public List<LibroDTO> GetLibrosByFilters(string? titulo, string? autor, string? categoria, string? idioma, string? genero)
        {
            var query = _context.Libros
                .Include(l => l.IdEditorialNavigation)
                .Include(l => l.IdIdiomaNavigation)
                .Include(l => l.AutoresLibros)
                    .ThenInclude(al => al.IdAutorNavigation)
                .Include(l => l.LibrosCategoria)
                    .ThenInclude(lc => lc.IdCategoriaNavigation)
                .Include(l => l.LibrosGeneros)
                    .ThenInclude(lg => lg.IdGeneroNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(titulo))
                query = query.Where(l => l.Titulo.Contains(titulo));

            if (!string.IsNullOrEmpty(autor))
                query = query.Where(l => l.AutoresLibros.Any(al => al.IdAutorNavigation.Nombre.Contains(autor)));

            if (!string.IsNullOrEmpty(categoria))
                query = query.Where(l => l.LibrosCategoria.Any(lc => lc.IdCategoriaNavigation.Categoria1.Contains(categoria)));

            if (!string.IsNullOrEmpty(idioma))
                query = query.Where(l => l.IdIdiomaNavigation.Idioma1.Contains(idioma));

            if (!string.IsNullOrEmpty(genero))
                query = query.Where(l => l.LibrosGeneros.Any(lg => lg.IdGeneroNavigation.Genero1.Contains(genero)));

            return query.Select(l => new LibroDTO
            {
                CodLibro = l.CodLibro,
                Titulo = l.Titulo,
                Editorial = l.IdEditorialNavigation.Editorial,
                Idioma = l.IdIdiomaNavigation.Idioma1,
                Autores = l.AutoresLibros.Select(al => al.IdAutorNavigation.Nombre).ToList(),
                Categorias = l.LibrosCategoria.Select(lc => lc.IdCategoriaNavigation.Categoria1).ToList(),
                Generos = l.LibrosGeneros.Select(lg => lg.IdGeneroNavigation.Genero1).ToList(),
                Precio = l.Precio,
                Stock = l.Stock
            }).ToList();
        }
    }
}
