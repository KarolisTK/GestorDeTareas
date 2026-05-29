using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;

namespace GestorDeTareas.Interfaces
{
    public interface INotificacionesService
    {
        Task CrearNotificacion(TiposNotificaciones tipoDeNotificacion, int idEmisor, int idReceptor);
        Task MarcarNotificacionesComoLeidas(int idNotificacion, int idUsuario);
        Task<List<ListarNotificacionesDTO>> ObtenerNotificacionesPorUsuario(int idUsuario);
        Task EnviarNotificacionAsync(int idUsuario, int idUsuarioReceptor, TiposNotificaciones tipoDeNotificacion);
    }
}
