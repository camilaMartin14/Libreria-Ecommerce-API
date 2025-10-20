using Libreria_API.DTOs;
using Libreria_API.Models;
using Libreria_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Libreria_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] ClienteDTO dto)
        {
            try
            {
                await _service.RegistrarAsync(dto);
                return Ok("Cliente registrado con éxito.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var cliente = await _service.LoginAsync(dto.Usuario, dto.Contraseña);
            if (cliente == null)
                return Unauthorized("Usuario o contraseña incorrectos.");

            return Ok(new { mensaje = "Login exitoso", idCliente = cliente.CodCliente });
        }
    }

}
