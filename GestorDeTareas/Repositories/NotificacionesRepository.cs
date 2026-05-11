using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;

namespace GestorDeTareas.Repositories
{
    public class NotificacionesRepository: Repository<Notificaciones>, INotificacionesRepository
    {
        public NotificacionesRepository(AppDbContext context) : base(context) { }
    }
}
