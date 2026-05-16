using GestorDeTareas.DTOs;

namespace GestorDeTareas.Interfaces
{
    public interface IAmigosService
    {
        Task<ObtenerDatosDeAmigoPorFriendTagDTO> BuscarAmigosPorFriendTag(string friendTag);
        Task AceptarSolicitudAmistad(int idUsuarioEmisor, int idUsuarioReceptor);
        Task<List<ListarAmigosDTO>> ListarTodosLosAmigos(int idEmisor);
        Task<List<SolicitudAmistadDto>> ListarSolicitudesDeAmistad(int id);
    }
}
