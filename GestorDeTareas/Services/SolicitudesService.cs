using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using GestorDeTareas.Repositories;

namespace GestorDeTareas.Services
{
    public class SolicitudesService
    {
        private readonly ISolicitudesRepository _solicitudesRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public SolicitudesService(ISolicitudesRepository solicitudesRepository, IUsuarioRepository usuarioRepository)
        {
            _solicitudesRepository = solicitudesRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task EnviarSolicitud(int idUsuarioEmisor, int idUsuarioReceptor, TiposSolicitudes tipoSolicitud, int? idEspacioDeTrabajoACompartir)
        {
            var envioCreado = new Solicitudes(idUsuarioEmisor, idUsuarioReceptor, tipoSolicitud, idEspacioDeTrabajoACompartir);
            await _solicitudesRepository.Guardar(envioCreado);
        }

        public async Task<Solicitudes> TramitarSolicitud(int idSolicitud, TipoEstadoSolicitud resolucionSolicitudoSolicitud, int idUsuario)
        {
            var solicitud = await _solicitudesRepository.ObtenerPorId(idSolicitud);
            if (solicitud is null)
                throw new KeyNotFoundException($"No existe una solicitud con id {idSolicitud}");
            if (solicitud.IdReceptor != idUsuario)
                throw new UnauthorizedAccessException("No tienes permiso para tramitar esta solicitud.");
            if (solicitud.TiposEstado != TipoEstadoSolicitud.Pendiente)
                throw new InvalidOperationException("Esta solicitud ya fue tramitada.");
            solicitud.TiposEstado = resolucionSolicitudoSolicitud;
            await _solicitudesRepository.Guardar(solicitud);
            return solicitud;
        }

        public async Task<List<SolicitudesDTO>> ListarSolicitudes(int id, TiposSolicitudes tiposSolicitudes)
        {
            return await _solicitudesRepository.ObtenerSolicitudesPendientes(id, tiposSolicitudes);
        }
    }
}
