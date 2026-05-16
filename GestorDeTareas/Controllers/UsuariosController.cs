using Azure.Messaging;
using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Interfaces;
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
        private readonly IUsuarioService _usuarioService;
        private readonly IAmigosService _amigosService;
        private readonly INotificacionesService _notificacionesService;
        private readonly ISolicitudesService _solicitudesService;

        public UsuariosController(IUsuarioService usuarioService, IAmigosService amigosService, INotificacionesService notificacionesService, ISolicitudesService solicitudesService)
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
        [HttpPost("EnviarSolicitud/{idUsuarioReceptor}/{tipoSolicitud}/{idespacioDeTrabajoACompartir?}")]
        public async Task<IActionResult> EnviarSolicitud(int idUsuarioReceptor, TiposSolicitudes tipoSolicitud, int? idEspacioDeTrabajoACompartir)
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _solicitudesService.EnviarSolicitud(idUsuario, idUsuarioReceptor, tipoSolicitud, idEspacioDeTrabajoACompartir);
            await _notificacionesService.CrearNotificacion(TiposNotificaciones.Solicitud, idUsuario, idUsuarioReceptor);
            return Ok();
        }

        [Authorize]
        [HttpPost("TramitarSolicitud/{idSolicitud}/{resolucionSolicitud}")]
        public async Task<IActionResult> TramitarSolicitud(int idSolicitud, TipoEstadoSolicitud resolucionSolicitud)
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _solicitudesService.TramitarSolicitud(idSolicitud, resolucionSolicitud, idUsuario);
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
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _notificacionesService.MarcarNotificacionesComoLeidas(idNotificacion, idUsuario);
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
