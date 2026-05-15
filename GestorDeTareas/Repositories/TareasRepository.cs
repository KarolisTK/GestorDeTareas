using GestorDeTareas.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorDeTareas.Repositories
{
    public class TareasRepository : Repository<Tarea>, ITareasRepository
    {
        public TareasRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Tarea>> ObtenerPorEspacioYUsuario(int idEspacioDeTrabajo, int idUsuario)
        {
            return await _context.Tareas
                .Where(t => t.EspacioDeTrabajoId == idEspacioDeTrabajo
                         && t.EspacioDeTrabajo.Usuarios
                             .Any(u => u.IdUsuario == idUsuario))
                .ToListAsync();
        }

    }
}
