using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorDeTareas.Repositories
{
    public class NotificacionesRepository: Repository<Notificaciones>, INotificacionesRepository
    {
        public NotificacionesRepository(AppDbContext context) : base(context) { }


        public async Task<List<ListarNotificacionesDTO>> ObtenerNotificacionesPorIdUsuario(int idUsuario)
        {
            return await _context.Notificaciones
                .Where(a => a.IdReceptor == idUsuario)
                .Select(a => new ListarNotificacionesDTO
                {
                    IdNotificacion = a.IdNotificacion,
                    FechaCreacionNotificacion = a.FechaCreacionNotificacion,
                    TituloNotificacion = a.TituloNotificacion,
                    ContenidoNotificacion = a.ContenidoNotificacion,
                    MarcadoComoLeido = a.MarcadoComoLeido
                })
                .ToListAsync();
        }
    }
}
