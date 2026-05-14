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
        private readonly SolicitudesService _solicitudesService;

        public UsuariosController(UsuarioService usuarioService, AmigosService amigosService, NotificacionesService notificacionesService, SolicitudesService solicitudesService)
        {
            _usuarioService = usuarioService;
            _amigosService = amigosService;
            _notificacionesService = notificacionesService;
            _solicitudesService = solicitudesService;
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
            await _solicitudesService.EnviarSolicitudAmistad(idUsuario, idUsuarioReceptor);
            await _notificacionesService.CrearNotificacion(TiposNotificaciones.Solicitud, idUsuario, idUsuarioReceptor);
            return Ok();
        }

        [Authorize]
        [HttpPost("TramitarSolicitudAmistad/{idPeticionAmistad}/{resolucionSolicitud}")]
        public async Task<IActionResult> TramitarSolicitudAmistad(int idPeticionAmistad, TipoEstadoSolicitud resolucionSolicitud)
        {
			var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			var tramite = await _solicitudesService.TramitarSolicitudAmistad(idPeticionAmistad, resolucionSolicitud, idUsuario);
            if (resolucionSolicitud == TipoEstadoSolicitud.Aceptado)
            {
                await _amigosService.AceptarSolicitudAmistad(tramite.IdEmisor, idUsuario);
                await _notificacionesService.CrearNotificacion(TiposNotificaciones.Solicitud, idUsuario, tramite.IdReceptor);
            }
                await _notificacionesService.CrearNotificacion(TiposNotificaciones.Rechazada, idUsuario, tramite.IdReceptor);
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
        public async Task<ActionResult<List<SolicitudesDTO>>> ListarSolicitudesDeAmistad()
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var Solicitudes = await  _solicitudesService.ListarSolicitudes(idUsuario , TiposSolicitudes.Amistad);
            return Solicitudes;
        }

        [Authorize]
        [HttpPost("MarcarNotificacionComoLeida")]
        public async Task<IActionResult> MarcarNotificacionComoLeida([FromBody] int idNotificacion)
        {
            await _notificacionesService.MarcarNotificacionesComoLeidas(idNotificacion);
            return Ok();
        }

        [Authorize]
        [HttpGet("ListarNotificacionesPorUsuario")]
        public async Task<ActionResult<List<ListarNotificacionesDTO>>> ListarNotificacionesPorUsuario()
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _notificacionesService.ObtenerNotificacionesPorUsuario(idUsuario);
        }
    }
}
