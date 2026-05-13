using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;

namespace GestorDeTareas.Services
{
    public class NotificacionesService
    {
        private readonly INotificacionesRepository _NotificacionesRepository;
        private readonly IUsuarioRepository _UsuarioRepository;

        public NotificacionesService(INotificacionesRepository notificacionesRepository, IUsuarioRepository usuarioRepository)
        {
            _NotificacionesRepository = notificacionesRepository;
            _UsuarioRepository = usuarioRepository;
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
            };
            var contenidoNotificacion = new Dictionary<TiposNotificaciones, string>
            {
                { TiposNotificaciones.Solicitud, "Tienes una nueva solicitud de amistad" },
                { TiposNotificaciones.Aceptada,  "Tu solicitud de amistad fue aceptada" },
                { TiposNotificaciones.Rechazada, "Tu solicitud de amistad fue rechazada" },
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
            _NotificacionesRepository.Guardar(notificacion);

        }

        public async Task MarcarNotificacionesComoLeidas( int idNotificacion)
        {
           var notificacionAMarcar = await _NotificacionesRepository.ObtenerPorId(idNotificacion);
           notificacionAMarcar.MarcadoComoLeido = true;
           _NotificacionesRepository.Guardar(notificacionAMarcar);
        }

        public async Task<List<ListarNotificacionesDTO>> ObtenerNotificacionesPorUsuario(int idUsuario)
        {
            return await _NotificacionesRepository.ObtenerNotificacionesPorIdUsuario(idUsuario);
        }
    }
}
