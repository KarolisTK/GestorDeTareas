using GestorDeTareas.DTOs;
using GestorDeTareas.Models;

namespace GestorDeTareas.Interfaces
{
    public interface INotificacionesRepository : IRepositorio<Notificaciones>
    {
        Task<List<ListarNotificacionesDTO>> ObtenerNotificacionesPorIdUsuario(int idUsuario);
    }
}
