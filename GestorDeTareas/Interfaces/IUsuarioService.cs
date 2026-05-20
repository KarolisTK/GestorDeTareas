using GestorDeTareas.DTOs;
using GestorDeTareas.Models;

namespace GestorDeTareas.Interfaces

{
    public interface IUsuarioService
    {
        Task<Usuario> ObtenerUnUsuarioPorID(int IdUsuario);
        Task<Usuario> ObtenerUsuarioPorCorreo(string correo);
        Task CrearUsuario(CrearUsuarioDTO dto);
        Task EditarUsuario(EditarUsuarioDTO dto, int IdUsuario);
        Task EliminarUsuario(int idUsuario);
    }
}
