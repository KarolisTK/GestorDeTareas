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
        public IActionResult CrearTareaUrgente([FromBody] CrearTareaUrgenteDTO dto)
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _tareaUrgenteService.CrearTareaUrgente(dto, idUsuario);
            return Ok();
        }

        [HttpPut("{id}/priorizar")]
        public IActionResult PriorizarTarea(int id, [FromBody] CrearTareaUrgenteDTO dto)
        {
            _tareaUrgenteService.PriorizarTarea(id, dto);
            return Ok();
        }

        [HttpPut("{id}/quitarPrioridad")]
        public IActionResult QuitarPrioridadTarea(int id, [FromBody] CrearTareaDTO dto)
        {
            _tareaUrgenteService.QuitarPrioridadTarea(id, dto);
            return Ok();
        }
    }
}
