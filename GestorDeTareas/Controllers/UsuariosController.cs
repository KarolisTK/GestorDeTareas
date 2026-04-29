using GestorDeTareas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestorDeTareas.Controllers
{
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("{correoUsuario}/{contrasenaUsuario}")]
        public IActionResult Login(string correoUsuario, string contrasenaUsuario)
        {
            _usuarioService.IniciarSesion(correoUsuario,contrasenaUsuario);
            return Ok();
        }
    }
}
