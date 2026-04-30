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
    public class TareasUrgentesController : Controller
    {
        private readonly TareaUrgenteService _tareaUrgenteService;
        public TareasUrgentesController(TareaUrgenteService tareaUrgenteService)
        { 
            _tareaUrgenteService = tareaUrgenteService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTareaUrgente([FromBody] CrearTareaUrgenteDTO dto)
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _tareaUrgenteService.CrearTareaUrgente(dto, idUsuario);
            return Ok();
        }

        [HttpPut("{id}/priorizar")]
        public async Task<IActionResult> PriorizarTarea(int id, [FromBody] CrearTareaUrgenteDTO dto)
        {
            await _tareaUrgenteService.PriorizarTarea(id, dto);
            return Ok();
        }

        [HttpPut("{id}/quitarPrioridad")]
        public async Task<IActionResult> QuitarPrioridadTarea(int id, [FromBody] TareaDTO dto)
        {
            await _tareaUrgenteService.QuitarPrioridadTarea(id, dto);
            return Ok();
        }
    }
}
