using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorDeTareas.Repositories
{
    public class TareasRepository : Repository<Tarea>, ITareasRepository
    {
        public TareasRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<ObtenerTareasDTO>> ObtenerPorEspacioYUsuario(int idEspacioDeTrabajo, int idUsuario)
        {
            return await _context.Tareas
                .Where(t => t.EspacioDeTrabajoId == idEspacioDeTrabajo && t.EspacioDeTrabajo.Usuarios
                .Any(u => u.IdUsuario == idUsuario) && t.EstaEliminado == false)
                .Select(o => new ObtenerTareasDTO
                {
                    IdTarea = o.IdTarea,
                    NombreTarea = o.NombreTarea,
                    DescripcionTarea = o.DescripcionTarea,
                    FechaCreacionTarea = o.FechaCreacionTarea,
                    EstadosTarea = o.EstadosTarea,
                    EstaEliminado = o.EstaEliminado,
                    TiposTarea = o.TiposTarea,
                    IdUsuarioDeLaTarea = o.IdUsuarioDeLaTarea,
                    EspacioDeTrabajoId = o.EspacioDeTrabajoId,

                })
                             
                .ToListAsync();
        }

    }
}
