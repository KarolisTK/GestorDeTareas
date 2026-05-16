using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Models;

namespace GestorDeTareas.Interfaces
{
    public interface ISolicitudesRepository : IRepositorio<Solicitudes>
    {
        Task<List<SolicitudesDTO>> ObtenerSolicitudesPendientes(int id, TiposSolicitudes tiposSolicitudes);
    }
}
