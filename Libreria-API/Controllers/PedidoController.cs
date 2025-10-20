using Libreria_API.DTOs;
using Libreria_API.Models;
using Libreria_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Libreria_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _service;
        public PedidoController(IPedidoService service)
        {
            _service = service;
        }

        // GET: api/Pedido
        [HttpGet]
        public ActionResult<List<PedidoDTO>> GetAll([FromQuery] DateTime? fecha, [FromQuery] int? codigoCliente)
        {
            var pedidos = _service.GetAll(fecha, codigoCliente);
            return Ok(pedidos);
        }

        // GET: api/Pedido/5
        [HttpGet("{id}")]
        public ActionResult<PedidoDTO> GetById(int id)
        {
            var pedido = _service.GetPedidoById(id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }

        // POST: api/Pedido
        [HttpPost]
        public ActionResult<PedidoDTO> Create([FromBody] Pedido pedido)
        {
            _service.Create(pedido);
            // Asumiendo que el pedido ya tiene NroPedido asignado después de SaveChanges
            var dto = _service.GetPedidoById(pedido.NroPedido);
            return CreatedAtAction(nameof(GetById), new { id = pedido.NroPedido }, dto);
        }

        // GET: api/Pedido/5/estado
        [HttpGet("{nroPedido}/estado")]
        public ActionResult<string> GetEstadoActual(int nroPedido)
        {
            var estado = _service.ObtenerEstadoActualPedido(nroPedido);
            return Ok(estado);
        }

        // PUT: api/Pedido/5/estado
        [HttpPut("{nroPedido}/estado")]
        public IActionResult UpdateStatus(int nroPedido, [FromQuery] int nuevoEstadoId, [FromQuery] string observaciones)
        {
            try
            {
                _service.UpdateStatus(nroPedido, nuevoEstadoId, observaciones);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
