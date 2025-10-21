using Libreria_API.DTOs;
using Libreria_API.Models;
using Libreria_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        // 🔹 REGISTRO DE NUEVO CLIENTE
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistroDTO dto)
        {
            try
            {
                // 1️⃣ Validación básica de campos
                if (string.IsNullOrWhiteSpace(dto.NombreUsuario) || string.IsNullOrWhiteSpace(dto.Contrasena))
                    return BadRequest("El usuario y la contraseña son obligatorios.");

                if (string.IsNullOrWhiteSpace(dto.Nombre) || string.IsNullOrWhiteSpace(dto.Apellido))
                    return BadRequest("El nombre y apellido son obligatorios.");

                // 2️⃣ Creación del objeto Usuario (solo los datos de cuenta)
                var usuario = new Usuario
                {
                    NombreUsuario = dto.NombreUsuario,
                    ContrasenaHash = dto.Contrasena, // se hashea en el servicio
                    Rol = "Cliente",
                    FechaAlta = DateTime.Now
                };

                // 3️⃣ Creación del objeto Cliente (datos personales)
                var cliente = new Cliente
                {
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    NroDoc = dto.NroDoc,
                    IdTipoDoc = dto.IdTipoDoc,
                    IdSexo = dto.IdSexo,
                    IdNacionalidad = dto.IdNacionalidad,
                    FechaRegistro = DateTime.Now,
                    FechaNacimiento = dto.FechaNacimiento,
                    IdBarrio = dto.IdBarrio,
                    Calle = dto.Calle,
                    Nro = dto.Nro,
                    Piso = dto.Piso,
                    Dpto = dto.Dpto,
                    Cp = dto.Cp,
                    Email = dto.Email
                };

                // 4️⃣ Registrar cliente + usuario
                await _service.RegistrarAsync(cliente, usuario);

                // 5️⃣ Retornar respuesta con código 201 (Created)
                return CreatedAtAction(nameof(Login), new { usuario = dto.NombreUsuario },
                    new { mensaje = "Cliente registrado con éxito." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // 🔹 LOGIN DE CLIENTE
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            // 1️⃣ Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(dto.Usuario) || string.IsNullOrWhiteSpace(dto.Contraseña))
                return BadRequest("Usuario y contraseña requeridos.");

            // 2️⃣ Llamar al servicio para verificar credenciales
            var cliente = await _service.LoginAsync(dto.Usuario, dto.Contraseña);

            // 3️⃣ Manejar credenciales incorrectas
            if (cliente == null)
                return Unauthorized("Usuario o contraseña incorrectos.");

            // 4️⃣ Respuesta exitosa (solo datos seguros)
            return Ok(new
            {
                mensaje = "Login exitoso",
                idCliente = cliente.CodCliente,
                nombre = cliente.Nombre,
                apellido = cliente.Apellido
            });
        }
    }
}
