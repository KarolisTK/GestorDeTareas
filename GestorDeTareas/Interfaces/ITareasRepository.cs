using GestorDeTareas.DTOs;

namespace GestorDeTareas.Interfaces
{
    public interface ITareasRepository: IRepositorio<Tarea> 
    {
        Task<List<ObtenerTareasDTO>> ObtenerPorEspacioYUsuario(int idEspacioDeTrabajo, int idUsuario);
    }
}
