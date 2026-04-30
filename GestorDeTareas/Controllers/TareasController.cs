using GestorDeTareas.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestorDeTareas.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly TareaService _tareaService;
        public TareasController(TareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [HttpGet]
        public IActionResult ObtenerTodas()
        {
            var tareas = _tareaService.ObtenerTodas();
            return Ok(tareas);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerSoloUnaPorId(int id)
        {
            var tarea = _tareaService.ObtenerUnaTareaPorID(id);
            return Ok(tarea);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] CrearTareaDTO dto)
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _tareaService.CrearTarea(dto, idUsuario);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] EditarTareaDTO dto)
        {
            _tareaService.EditarTarea(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            _tareaService.EliminarTarea(id);
            return NoContent();
        }
    }
}