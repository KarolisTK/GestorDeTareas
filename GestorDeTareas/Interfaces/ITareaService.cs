using GestorDeTareas.DTOs;

namespace GestorDeTareas.Interfaces
{
    public interface ITareaService
    {
        Task<List<Tarea>> ObtenerTodas(int idEspacioDeTrabajo, int idUsuario);
        Task<Tarea> ObtenerUnaTareaPorID(int idTarea);
        Task CrearTarea(TareaDTO dto, int idUsuario);
        Task EditarTarea(int id, EditarTareaDTO dto, int idUsuario);
        Task EliminarTarea(int id);
    }
}
