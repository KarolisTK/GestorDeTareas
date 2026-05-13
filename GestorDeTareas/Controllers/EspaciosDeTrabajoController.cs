using GestorDeTareas.DTOs;
using GestorDeTareas.Models;
using GestorDeTareas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestorDeTareas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspaciosDeTrabajoController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly AmigosService _amigosService;
        private readonly NotificacionesService _notificacionesService;
        private readonly EspaciosDeTrabajoService _espaciosDeTrabajoService;

        public EspaciosDeTrabajoController(UsuarioService usuarioService, AmigosService amigosService
            , NotificacionesService notifcacionesService, EspaciosDeTrabajoService espaciosDeTrabajoService)
        {
            _usuarioService = usuarioService;
            _amigosService = amigosService;
            _notificacionesService = notifcacionesService;
            _espaciosDeTrabajoService = espaciosDeTrabajoService;
        }

        [Authorize]
        [HttpPost("CrearNuevoEspacioDeTrabajo")]
        public async Task<IActionResult> CrearNuevoEspacioDeTrabajo([FromBody] CrearNuevoEspacioDeTrabajoDTO dto)
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _espaciosDeTrabajoService.CrearEspacioDeTrabajo(idUsuario, dto);
            return Ok();
        }

    }
}
