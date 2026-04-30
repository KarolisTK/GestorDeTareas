using GestorDeTareas.DTOs;
using GestorDeTareas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestorDeTareas.Controllers
{
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
            _tareaUrgenteService.CrearTareaUrgente(dto);
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
