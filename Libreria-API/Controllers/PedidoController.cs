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
        public PedidoController(IPedidoService service) => _service = service;

        [HttpGet]
        public ActionResult<List<PedidoDTO>> GetAll([FromQuery] DateTime? fecha, [FromQuery] int? codigoCliente)
        {
            return Ok(_service.GetAll(fecha, codigoCliente));
        }

        [HttpPost]
        public ActionResult<PedidoDTO> Create([FromBody] Pedido pedido)
        {
            // Guardar pedido
            _service.Create(pedido);

            // Recargar pedido con relaciones
            var pedidoRecargado = _service.GetPedidoById(pedido.NroPedido);
            if (pedidoRecargado == null)
                return BadRequest("No se pudo recuperar el pedido después de crearlo.");

            return StatusCode(201, pedidoRecargado);
        }

        [HttpGet("{nroPedido}/estado")]
        public ActionResult<string> GetEstadoActual(int nroPedido)
        {
            var estado = _service.ObtenerEstadoActualPedido(nroPedido);
            return Ok(estado);
        }

        [HttpPut("{nroPedido}/estado")]
        public IActionResult UpdateStatus(int nroPedido, [FromBody] UpdateEstadoDTO dto)
        {
            try
            {
                _service.UpdateStatus(nroPedido, dto.NuevoEstadoId, dto.Observaciones);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}