using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Exceptions;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using GestorDeTareas.Repositories;

namespace GestorDeTareas.Services
{
    public class SolicitudesService : ISolicitudesService
    {
        private readonly ISolicitudesRepository _solicitudesRepository;
        private readonly IAmigosRepository _amigosRepository;
        private readonly IAmigosService _amigosService;
        private readonly INotificacionesService _notificacionesService;
        private readonly IEspaciosDeTrabajoService _espaciosDeTrabajoService;

        public SolicitudesService(
            ISolicitudesRepository solicitudesRepository,
            IAmigosRepository amigosRepository,
            IAmigosService amigosService,
            INotificacionesService notificacionesService,
            IEspaciosDeTrabajoService espaciosDeTrabajoService)
        {
            _solicitudesRepository = solicitudesRepository;
            _amigosRepository = amigosRepository;
            _amigosService = amigosService;
            _notificacionesService = notificacionesService;
            _espaciosDeTrabajoService = espaciosDeTrabajoService;
        }

        public async Task EnviarSolicitud(int idUsuarioEmisor, int idUsuarioReceptor, TiposSolicitudes tipoSolicitud, int? idEspacioDeTrabajoACompartir)
        {
            if (idUsuarioEmisor == idUsuarioReceptor)
                throw new ConflictException("No puedes enviarte una solicitud a ti mismo.");

            if (tipoSolicitud == TiposSolicitudes.Amistad)
            {
                var amigos = await _amigosRepository.ObtenerAmigosDeUsuario(idUsuarioEmisor);
                var yaEsAmigo = amigos.Any(a =>
                    a.IdEmisor == idUsuarioReceptor || a.IdReceptor == idUsuarioReceptor);
                if (yaEsAmigo)
                    throw new ConflictException("Este usuario ya es tu amigo.");

                var solicitudesPendientes = await _solicitudesRepository.ObtenerSolicitudesPendientes(idUsuarioReceptor, TiposSolicitudes.Amistad);
                var solicitudYaExiste = solicitudesPendientes.Any(s => s.IdSolicitante == idUsuarioEmisor);
                if (solicitudYaExiste)
                    throw new FriendException("Ya tienes una solicitud de amistad pendiente con este usuario.", 455);
            }

            if (tipoSolicitud == TiposSolicitudes.EspacioDeTrabajo)
            {
                if (idEspacioDeTrabajoACompartir == null)
                    throw new ConflictException("Debes indicar el espacio de trabajo a compartir.");

                var yaEstaEnElEspacio = await _espaciosDeTrabajoService
                    .MostrarEspaciosDeTrabajoPorUsuario(idUsuarioReceptor);

                if (yaEstaEnElEspacio.Any(e => e.IdEspacioDeTrabajo == idEspacioDeTrabajoACompartir))
                    throw new ConflictException("Este usuario ya pertenece a ese espacio de trabajo.");

                var solicitudesPendientes = await _solicitudesRepository
                    .ObtenerSolicitudesPendientes(idUsuarioReceptor, TiposSolicitudes.EspacioDeTrabajo);

                var yaInvitado = solicitudesPendientes.Any(s => s.Estado == TipoEstadoSolicitud.Pendiente);

                if (yaInvitado)
                    throw new SolicitudPendienteException("Este usuario ya ha sido invitado a ese espacio de trabajo.", 456);
            }

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
                    IdEspacioDeTrabajo = solicitud.IdEspacioDeTrabajoACompartir.Value,
                    IdUsuario = idUsuario
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
