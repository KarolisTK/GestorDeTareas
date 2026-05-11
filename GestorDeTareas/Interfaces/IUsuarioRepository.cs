using GestorDeTareas.Models;

namespace GestorDeTareas.Interfaces
{
    public interface IUsuarioRepository : IRepositorio<Usuario>
    {
        Task<Usuario> ObtenerPorFriendTag(string friendTag);
    }
}
