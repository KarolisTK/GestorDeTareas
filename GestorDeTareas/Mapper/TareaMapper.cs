using GestorDeTareas.DTOs;
namespace GestorDeTareas.Mapper
{
    public static class TareaMapper
    {
        public static Tarea CrearEntidad(CrearTareaDTO dto)
        {
            var tarea = new Tarea();
            return tarea.CrearTareaConDto(dto);
            
        }
        public static void ModificarEntidad(Tarea tarea, EditarTareaDTO dto)
        {
            tarea.EditarTareaConDTO( dto);
        }

        public static void EliminarEntidad(Tarea tarea)
        {
            tarea.MarcarTareaComoEliminada(tarea);
        }
    }
}
