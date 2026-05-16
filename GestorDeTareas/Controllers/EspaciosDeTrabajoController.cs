using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
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
        private readonly IEspaciosDeTrabajoService _espaciosDeTrabajoService;

        public EspaciosDeTrabajoController(IEspaciosDeTrabajoService espaciosDeTrabajoService)
        {
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

        [Authorize]
        [HttpGet("MostrarEspaciosDeTrabajoPorUsuario")]
        public async Task <ActionResult<List<MostrarEspaciosDeTrabajoDTO>>> MostrarEspaciosDeTrabajoPorUsuario()
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _espaciosDeTrabajoService.MostrarEspaciosDeTrabajoPorUsuario(idUsuario);
        }
    }
}
