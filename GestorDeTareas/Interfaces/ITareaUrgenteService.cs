using GestorDeTareas.DTOs;

namespace GestorDeTareas.Interfaces
{
    public interface ITareaUrgenteService
    {
        Task PriorizarTarea(int id, CrearTareaUrgenteDTO dto);
        Task QuitarPrioridadTarea(int id, TareaDTO dto);
        Task CrearTareaUrgente(CrearTareaUrgenteDTO dto, int idUsuario);
    }
}
