using GestorDeTareas.DTOs;

namespace GestorDeTareas.Interfaces
{
    public interface ITareaUrgenteService
    {
        Task PriorizarTarea(int id, CrearTareaUrgenteDTO dto);
        Task QuitarPrioridadTarea(int id, CrearTareaDTO dto);
        Task CrearTareaUrgente(CrearTareaUrgenteDTO dto, int idUsuario);
    }
}
