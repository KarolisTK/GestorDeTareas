using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
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
        private readonly ITareaService _tareaService;
        public TareasController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [HttpGet("ObtenerTodasLasTareas/{idEspacioDeTrabajo}")]
        public async Task<IActionResult> ObtenerTodas(int idEspacioDeTrabajo)
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var tareas = await _tareaService.ObtenerTodas(idEspacioDeTrabajo, idUsuario);
            return Ok(tareas);
        }

        [HttpGet("ObtenerTareaConId/{id}")]
        public async Task<IActionResult> ObtenerSoloUnaPorId(int id)
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var tarea = await _tareaService.ObtenerUnaTareaPorID(id);
            if (tarea.IdUsuarioDeLaTarea != idUsuario)
                return Forbid();
            return Ok(tarea);
        }

        [HttpPost("CrearTarea")]
        public async Task<IActionResult> Crear([FromBody] CrearTareaDTO dto)
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _tareaService.CrearTarea(dto, idUsuario);
            return Ok();
        }

        [HttpPut("EditarTarea{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] EditarTareaDTO dto)
        {
            var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _tareaService.EditarTarea(id, dto, idUsuario);
            return NoContent();
        }

        [HttpDelete("EliminarTarea{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _tareaService.EliminarTarea(id);
            return NoContent();
        }
    }
}