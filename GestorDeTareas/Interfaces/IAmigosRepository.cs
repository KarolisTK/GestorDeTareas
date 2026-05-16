using GestorDeTareas.DTOs;
using GestorDeTareas.Models;
using GestorDeTareas.Repositories;

namespace GestorDeTareas.Interfaces
{
    public interface IAmigosRepository : IRepositorio<Amigos>
    {
        Task<List<SolicitudAmistadDto>> ObtenerSolicitudesPendientes(int id);

        Task<List<Amigos>> ObtenerAmigosDeUsuario(int idUsuario);
    }
}
