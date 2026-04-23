using GestorDeTareas.DTOs;
namespace GestorDeTareas.Mapper
{
    public static class TareaMapper
    {
        public static Tarea CrearEntidad(CrearTareaDTO dto)
        {
            return new Tarea
            {
                NombreTarea = dto.NombreTarea,
                DescripcionTarea = dto.DescripcionTarea,
                FechaCreacionTarea = dto.FechaCreacionTarea,
                EstadosTarea = dto.EstadosTarea,
                EstaEliminado = dto.EstaEliminado,
                TiposTarea = dto.TiposTarea,
                IdUsuarioDeLaTarea = Sesion.IdUsuarioSesionActiva

            };

        }
        public static void ModificarEntidad(Tarea tarea, EditarTareaDTO dto)
        {
            tarea.NombreTarea = dto.NombreTarea ?? tarea.NombreTarea;
            tarea.DescripcionTarea = dto.DescripcionTarea ?? tarea.DescripcionTarea;
            tarea.EstadosTarea = dto.EstadosTarea ?? tarea.EstadosTarea;
            tarea.EstaEliminado = dto.EstaEliminado ?? tarea.EstaEliminado;
            tarea.TiposTarea = dto.TiposTarea ?? tarea.TiposTarea;
        }

        public static void EliminarEntidad(Tarea tarea)
        {
            tarea.EstaEliminado = true;
        }
    }
}
