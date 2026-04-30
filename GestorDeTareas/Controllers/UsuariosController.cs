using GestorDeTareas.DTOs;
using GestorDeTareas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestorDeTareas.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public IActionResult Crear([FromBody] UsuarioDTO dto)
        {
            _usuarioService.CrearUsuario(dto);
            return Ok();
        }

        [HttpPut]
        public IActionResult Editar([FromBody] EditarUsuarioDTO dto)
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _usuarioService.EditarUsuario(dto, idUsuario);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Eliminar()
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _usuarioService.EliminarUsuario(idUsuario);
            return NoContent();
        }
    }
}
