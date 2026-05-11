using Azure.Messaging;
using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Models;
using GestorDeTareas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestorDeTareas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly AmigosService _amigosService;
        private readonly NotificacionesService _notificacionesService;

        public UsuariosController(UsuarioService usuarioService, AmigosService amigosService, NotificacionesService notificacionesService)
        {
            _usuarioService = usuarioService;
            _amigosService = amigosService;
            _notificacionesService = notificacionesService;
        }

        [HttpPost("CrearUsuario")]
        [AllowAnonymous]
        public async Task<IActionResult> Crear([FromBody] UsuarioDTO dto)
        {
            await _usuarioService.CrearUsuario(dto);
            return Ok();
        }

        [Authorize]
        [HttpPut("EditarUsuario")]
        public async Task<IActionResult> Editar([FromBody] EditarUsuarioDTO dto)
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _usuarioService.EditarUsuario(dto, idUsuario);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("EliminarUsuario")]
        public async Task<IActionResult> Eliminar()
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _usuarioService.EliminarUsuario(idUsuario);
            return NoContent();
        }

        [Authorize]
        [HttpGet("EncontrarAmigoPorFriendTag/{friendTag}")]
        public async Task<ActionResult<Usuario>> EncontrarAmigoPorFriendTag(string friendTag)
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usuario = await _usuarioService.ObtenerUnUsuarioPorID(idUsuario);
            var amigoEncontrado = await _amigosService.BuscarAmigosPorFriendTag(friendTag);
            if(amigoEncontrado == null)
            {
                return NotFound("No se encontró ningún usuario con ese FriendTag.");
            }
            if(amigoEncontrado.FriendTag == usuario.FriendTag)
            {
                return BadRequest("No puedes añadirte a ti mismo como amigo.");
            }
            return Ok(amigoEncontrado);
        }

        [Authorize]
        [HttpPost("EnviarSolicitudAmistad/{idUsuarioReceptor}")]
        public async Task<IActionResult> EnviarSolicitudAmistad(int idUsuarioReceptor)
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _amigosService.EnviarSolicitudAmistad(idUsuario, idUsuarioReceptor);
            await _notificacionesService.CrearNotificacion(TiposNotificaciones.Solicitud, idUsuario, idUsuarioReceptor);
            return Ok();
        }

        [Authorize]
        [HttpPost("TramitarSolicitudAmistad/{idPeticionAmistad}/{ResolucionSolicitudAmistad}")]
        public async Task<IActionResult> TramitarSolicitudAmistad(int idPeticionAmistad, TiposEstadoAmistad ResolucionSolicitudAmistad)
        {
			var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			await _amigosService.TramitarSolicitudAmistad(idPeticionAmistad, ResolucionSolicitudAmistad, idUsuario);
            return Ok();
        }

		[Authorize]
		[HttpGet("ListarTodosLosAmigos")]
		public async Task<ActionResult<List<ListarAmigosDTO>>> ListarTodosLosAmigos()
		{
			var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _amigosService.ListarTodosLosAmigos(idUsuario);
		}

        [Authorize]
        [HttpGet("ListarSolicitudesDeAmistad")]
        public async Task<ActionResult<List<SolicitudAmistadDto>>> ListarSolicitudesDeAmistad()
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var Solicitudes = await  _amigosService.ListarSolicitudesDeAmistad(idUsuario);
            return Solicitudes;
        }
    }
}
