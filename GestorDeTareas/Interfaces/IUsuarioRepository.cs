using GestorDeTareas.Models;
using GestorDeTareas.Repositories;

namespace GestorDeTareas.Interfaces
{
    public interface IUsuarioRepository : IRepositorio<Usuario>
    {
        Task<Usuario> ObtenerPorFriendTag(string friendTag);
    }
}
