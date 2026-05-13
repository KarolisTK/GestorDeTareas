using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;

namespace GestorDeTareas.Repositories
{
    public class EspaciosDeTrabajoRepository : Repository<EspaciosDeTrabajo>, IEspaciosDeTrabajoRepository
    {
        public EspaciosDeTrabajoRepository(AppDbContext context) : base(context) { }
    }
}
