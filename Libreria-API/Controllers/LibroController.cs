using Libreria_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Libreria_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly ILibroService _service;
        public LibroController(ILibroService service)
        {
            _service = service;
        }

        [HttpGet("filtrar")]
        public IActionResult BuscarLibros(string? titulo, string? autor, string? categoria, string? idioma, string? genero)
        {
            var libros = _service.GetLibrosByFilters(titulo, autor, categoria, idioma, genero);
            return Ok(libros);
        }


        // GET: api/<LibroController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
