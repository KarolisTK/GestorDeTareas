using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Models;
namespace GestorDeTareas.Mapper
{
    public static class TareaMapper
    {
        public static Tarea CrearEntidad(TareaDTO dto, int idusuario)
        {
            return new Tarea
            (
                dto.NombreTarea,
                dto.DescripcionTarea,
                dto.FechaCreacionTarea,
                dto.EstadosTarea,
                dto.EstaEliminado,
                dto.TiposTarea,
                idusuario,
                dto.IdEspacioDeTrabajo

            );

        }
        public static void ModificarEntidad(Tarea tarea, EditarTareaDTO dto)
        {
            tarea.NombreTarea = dto.NombreTarea ?? tarea.NombreTarea;
            tarea.DescripcionTarea = dto.DescripcionTarea ?? tarea.DescripcionTarea;
            tarea.EstadosTarea = dto.EstadosTarea ?? tarea.EstadosTarea;
            tarea.EstaEliminado = dto.EstaEliminado ?? tarea.EstaEliminado;
            tarea.TiposTarea = dto.TiposTarea ?? tarea.TiposTarea;
        }

        public static Tarea ModificarEntidadDeTareaUrgente(Tarea tarea, TareaDTO dto, TareaUrgente tareaUrgente)
        {
            tarea.NombreTarea = dto.NombreTarea ?? tareaUrgente.NombreTarea;
            tarea.DescripcionTarea = dto.DescripcionTarea ?? tareaUrgente.DescripcionTarea;
            tarea.FechaCreacionTarea = tareaUrgente.FechaCreacionTarea;
            tarea.EstadosTarea = dto.EstadosTarea ?? tareaUrgente.EstadosTarea;
            tarea.EstaEliminado = false;
            tarea.TiposTarea = TiposTarea.Simple;
            tarea.IdUsuarioDeLaTarea = tareaUrgente.IdUsuarioDeLaTarea;
            return tarea;
        }

        public static void EliminarEntidad(Tarea tarea)
        {
            tarea.EstaEliminado = true;
        }
    }
}
