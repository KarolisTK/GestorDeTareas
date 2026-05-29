using GestorDeTareas.DTOs;
using GestorDeTareas.Models;

namespace GestorDeTareas.Mapper
{
    public class ObtenerTareaPorIdTareaMapper
    {
        public static ObtenerTareasDTO Map(Tarea tarea)
        {
            return new ObtenerTareasDTO
            {
                IdTarea = tarea.IdTarea,
                NombreTarea = tarea.NombreTarea,
                DescripcionTarea = tarea.DescripcionTarea,
                FechaCreacionTarea = tarea.FechaCreacionTarea,
                EstadosTarea = tarea.EstadosTarea,
                EstaEliminado = tarea.EstaEliminado,
                TiposTarea = tarea.TiposTarea,
                IdUsuarioDeLaTarea = tarea.IdUsuarioDeLaTarea,
                EspacioDeTrabajoId = tarea.EspacioDeTrabajoId
            };
        }
    }
}
