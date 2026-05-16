using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;

namespace GestorDeTareas.Interfaces
{
    public interface ISolicitudesService
    {
        Task EnviarSolicitud(int idUsuarioEmisor, int idUsuarioReceptor, TiposSolicitudes tipoSolicitud, int? idEspacioDeTrabajoACompartir);
        Task TramitarSolicitud(int idSolicitud, TipoEstadoSolicitud resolucion, int idUsuario);
        Task<List<SolicitudesDTO>> ListarSolicitudes(int id, TiposSolicitudes tiposSolicitudes);
    }
}
