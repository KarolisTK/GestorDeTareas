using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestorDeTareas.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TareasUrgentesController : Controller
    {
        private readonly ITareaUrgenteService _tareaUrgenteService;
        public TareasUrgentesController(ITareaUrgenteService tareaUrgenteService)
        { 
            _tareaUrgenteService = tareaUrgenteService;
        }

        [HttpPost("CrearTareaUrgente")]
        public async Task<IActionResult> CrearTareaUrgente([FromBody] CrearTareaUrgenteDTO dto)
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _tareaUrgenteService.CrearTareaUrgente(dto, idUsuario);
            return Ok();
        }

        [HttpPut("PriorizarTarea{id}")]
        public async Task<IActionResult> PriorizarTarea(int id, [FromBody] CrearTareaUrgenteDTO dto)
        {
            await _tareaUrgenteService.PriorizarTarea(id, dto);
            return Ok();
        }

        [HttpPut("quitarPrioridad{id}")]
        public async Task<IActionResult> QuitarPrioridadTarea(int id, [FromBody] CrearTareaDTO dto)
        {
            await _tareaUrgenteService.QuitarPrioridadTarea(id, dto);
            return Ok();
        }
    }
}
