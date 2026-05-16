using GestorDeTareas.DTOs;
using GestorDeTareas.Models;

namespace GestorDeTareas.Interfaces
{
    public interface IEspaciosDeTrabajoRepository : IRepositorio<EspaciosDeTrabajo>
    {
        Task<List<MostrarEspaciosDeTrabajoDTO>> MostrarEspaciosDeTrabajo(int idUsuario);
    }
}
