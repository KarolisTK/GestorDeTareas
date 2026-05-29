using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorDeTareas.Repositories
{
    public class EspaciosDeTrabajoRepository : Repository<EspaciosDeTrabajo>, IEspaciosDeTrabajoRepository
    {
        public EspaciosDeTrabajoRepository(AppDbContext context) : base(context) { }

        public async Task<List<MostrarEspaciosDeTrabajoDTO>> MostrarEspaciosDeTrabajo(int idUsuario)
        {
            return await _context.EspaciosDeTrabajo
                .Include(e => e.Usuarios)
                .Where(e => e.Usuarios.Any(u => u.IdUsuario == idUsuario) && e.EstaEliminado == false)
                .Select(a => new MostrarEspaciosDeTrabajoDTO
                {
                    IdEspacioDeTrabajo = a.Id,
                    NombreEspacioTrabajo = a.Nombre,
                    EstaElimiado = a.EstaEliminado
                })
                .ToListAsync();
        }
    }
}
