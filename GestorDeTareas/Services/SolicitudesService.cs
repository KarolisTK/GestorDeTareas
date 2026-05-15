using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Exceptions;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using GestorDeTareas.Repositories;

namespace GestorDeTareas.Services
{
    public class SolicitudesService
    {
        private readonly ISolicitudesRepository _solicitudesRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly AmigosService _amigosService;
        private readonly NotificacionesService _notificacionesService;
        private readonly EspaciosDeTrabajoService _espaciosDeTrabajoService;

        public SolicitudesService(
            ISolicitudesRepository solicitudesRepository,
            IUsuarioRepository usuarioRepository,
            AmigosService amigosService,
            NotificacionesService notificacionesService,
            EspaciosDeTrabajoService espaciosDeTrabajoService)
        {
            _solicitudesRepository = solicitudesRepository;
            _usuarioRepository = usuarioRepository;
            _amigosService = amigosService;
            _notificacionesService = notificacionesService;
            _espaciosDeTrabajoService = espaciosDeTrabajoService;
        }

        public async Task EnviarSolicitud(int idUsuarioEmisor, int idUsuarioReceptor, TiposSolicitudes tipoSolicitud, int? idEspacioDeTrabajoACompartir)
        {
            var envioCreado = new Solicitudes(idUsuarioEmisor, idUsuarioReceptor, tipoSolicitud, idEspacioDeTrabajoACompartir);
            await _solicitudesRepository.Guardar(envioCreado);
        }

        public async Task TramitarSolicitud(int idSolicitud, TipoEstadoSolicitud resolucion, int idUsuario)
        {
            var solicitud = await _solicitudesRepository.ObtenerPorId(idSolicitud);
            if (solicitud is null)
                throw new NotFoundException($"No existe una solicitud con id {idSolicitud}");
            if (solicitud.IdReceptor != idUsuario)
                throw new ForbiddenException("No tienes permiso para tramitar esta solicitud.");
            if (solicitud.TiposEstado != TipoEstadoSolicitud.Pendiente)
                throw new ConflictException("Esta solicitud ya fue tramitada.");

            solicitud.TiposEstado = resolucion;
            await _solicitudesRepository.Guardar(solicitud);

            if (resolucion == TipoEstadoSolicitud.Aceptado && solicitud.TiposSolicitudes == TiposSolicitudes.Amistad)
            {
                await _amigosService.AceptarSolicitudAmistad(solicitud.IdEmisor, idUsuario);
                await _notificacionesService.CrearNotificacion(TiposNotificaciones.Aceptada, idUsuario, solicitud.IdEmisor);
            }
            else if (resolucion == TipoEstadoSolicitud.Aceptado && solicitud.TiposSolicitudes == TiposSolicitudes.EspacioDeTrabajo)
            {
                var dto = new AniadirNuevoUsuarioAlEspacioDeTrabajoDTO
                {
                    idEspacioDeTrabajo = solicitud.IdEspacioDeTrabajoACompartir.Value,
                    idUsuario = idUsuario
                };
                await _espaciosDeTrabajoService.AniadirNuevoUsuarioAlEspacioDeTrabajo(dto);
                await _notificacionesService.CrearNotificacion(TiposNotificaciones.EntradaAEspacioDeTrabajo, idUsuario, solicitud.IdEmisor);
            }
            else
            {
                await _notificacionesService.CrearNotificacion(TiposNotificaciones.Rechazada, idUsuario, solicitud.IdEmisor);
            }
        }

        public async Task<List<SolicitudesDTO>> ListarSolicitudes(int id, TiposSolicitudes tiposSolicitudes)
        {
            return await _solicitudesRepository.ObtenerSolicitudesPendientes(id, tiposSolicitudes);
        }
    }
}
