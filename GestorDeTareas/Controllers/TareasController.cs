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
        public async Task<IActionResult> ObtenerTodas()
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var tareas = await _tareaService.ObtenerTodas(idUsuario);
            return Ok(tareas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerSoloUnaPorId(int id)
        {
            var tarea = await _tareaService.ObtenerUnaTareaPorID(id);
            return Ok(tarea);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] TareaDTO dto)
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _tareaService.CrearTarea(dto, idUsuario);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] EditarTareaDTO dto)
        {
            await _tareaService.EditarTarea(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _tareaService.EliminarTarea(id);
            return NoContent();
        }
    }
}