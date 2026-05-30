using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Exceptions;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using Resend;

namespace GestorDeTareas.Services
{
    public class NotificacionesService : INotificacionesService
    {
        private readonly INotificacionesRepository _NotificacionesRepository;
        private readonly IUsuarioRepository _UsuarioRepository;
        private readonly IResend _resend;

        public NotificacionesService(INotificacionesRepository notificacionesRepository, IUsuarioRepository usuarioRepository, IResend resend)
        {
            _NotificacionesRepository = notificacionesRepository;
            _UsuarioRepository = usuarioRepository;
            _resend = resend;
        }

        public async Task CrearNotificacion(TiposNotificaciones tipoDeNotificacion, int idEmisor, int idReceptor)
        {
            var usuarioEmisor = await _UsuarioRepository.ObtenerPorId(idEmisor);
            var usuarioReceptor = await _UsuarioRepository.ObtenerPorId(idReceptor);

            var titulosNotificacion = new Dictionary<TiposNotificaciones, string>
            {
                { TiposNotificaciones.Solicitud, "solicitud de amistad " + usuarioEmisor.NombreUsuario },
                { TiposNotificaciones.Aceptada, usuarioReceptor.NombreUsuario + " aceptada" },
                { TiposNotificaciones.Rechazada, usuarioReceptor.NombreUsuario +  " rechazada" },
                {TiposNotificaciones.EntradaAEspacioDeTrabajo, usuarioReceptor.NombreUsuario + "Ha entrado en tu espacio de trabajo" }
            };
            var contenidoNotificacion = new Dictionary<TiposNotificaciones, string>
            {
                { TiposNotificaciones.Solicitud, "Tienes una nueva solicitud de amistad" },
                { TiposNotificaciones.Aceptada,  "Tu solicitud de amistad fue aceptada" },
                { TiposNotificaciones.Rechazada, "Tu solicitud de amistad fue rechazada" },
                { TiposNotificaciones.EntradaAEspacioDeTrabajo, "El usuario ha entrado al espacio de trabajo" },
            };

            if (!contenidoNotificacion.ContainsKey(tipoDeNotificacion))
                throw new ArgumentException($"Tipo de notificación no reconocido: {tipoDeNotificacion}");

            var notificacion = new Notificaciones
            {
                IdEmisor = idEmisor,
                IdReceptor = idReceptor,
                FechaCreacionNotificacion = System.DateTime.UtcNow,
                TipoNotificacion = tipoDeNotificacion,
                TituloNotificacion = titulosNotificacion[tipoDeNotificacion],
                ContenidoNotificacion = contenidoNotificacion[tipoDeNotificacion],
                MarcadoComoLeido = false
            };
            await _NotificacionesRepository.Guardar(notificacion);

        }

        public async Task MarcarNotificacionesComoLeidas( int idNotificacion, int idUsuario)
        {
            var notificacion = await _NotificacionesRepository.ObtenerPorId(idNotificacion);
            if (notificacion is null)
                throw new NotFoundException("La notificación no existe");
            if (notificacion.IdReceptor != idUsuario)
                throw new ForbiddenException("No tienes permiso para marcar esta notificación");

            notificacion.MarcadoComoLeido = true;
            await _NotificacionesRepository.Guardar(notificacion);
        }

        public async Task<List<ListarNotificacionesDTO>> ObtenerNotificacionesPorUsuario(int idUsuario)
        {
            return await _NotificacionesRepository.ObtenerNotificacionesPorIdUsuario(idUsuario);
        }

        public async Task EnviarNotificacionAsync(int idUsuario, int idUsuarioReceptor, TiposNotificaciones tipoDeNotificacion)
        {
            var usuarioEmisor = await _UsuarioRepository.ObtenerPorId(idUsuario);
            var usuarioReceptor = await _UsuarioRepository.ObtenerPorId(idUsuarioReceptor);

            var titulosNotificacion = new Dictionary<TiposNotificaciones, string>
            {
                { TiposNotificaciones.Solicitud, "solicitud de amistad " + usuarioEmisor.NombreUsuario },
                { TiposNotificaciones.Aceptada, usuarioReceptor.NombreUsuario + " aceptada" },
                { TiposNotificaciones.Rechazada, usuarioReceptor.NombreUsuario +  " rechazada" },
                {TiposNotificaciones.EntradaAEspacioDeTrabajo, usuarioReceptor.NombreUsuario + "Ha entrado en tu espacio de trabajo" }
            };
            var contenidoNotificacion = new Dictionary<TiposNotificaciones, string>
            {
                { TiposNotificaciones.Solicitud, "Tienes una nueva solicitud de amistad de " + usuarioEmisor.NombreUsuario + " Entra en tus notificaciones de amistad en JustOneStep.DeKarolis.com para gestionar la solicitud." },
                { TiposNotificaciones.Aceptada,  "Tu solicitud de amistad fue aceptada" },
                { TiposNotificaciones.Rechazada, "Tu solicitud de amistad fue rechazada" },
            };

            await _resend.EmailSendAsync(new EmailMessage()
            {
                From = "noreply@justonestep.dekarolis.com",
                To = usuarioReceptor.CorreoUsuario,
                Subject = titulosNotificacion[tipoDeNotificacion],
                HtmlBody = contenidoNotificacion[tipoDeNotificacion],
            });
        }
    }
}
