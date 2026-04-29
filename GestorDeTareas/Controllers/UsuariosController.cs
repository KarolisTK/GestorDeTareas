using GestorDeTareas.DTOs;
using GestorDeTareas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestorDeTareas.Controllers
{
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
            _usuarioService.EditarUsuario(dto);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Eliminar()
        {
            _usuarioService.EliminarUsuario();
            return NoContent();
        }

        [HttpPost("{correoUsuario}/{contrasenaUsuario}")]
        public IActionResult Login(string correoUsuario, string contrasenaUsuario)
        {
            _usuarioService.IniciarSesion(correoUsuario,contrasenaUsuario);
            return Ok();
        }
    }
}
