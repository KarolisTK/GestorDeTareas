using GestorDeTareas.DTOs;
using GestorDeTareas.Models;
namespace GestorDeTareas.Mapper
{
    public class UsuarioMapper
    {
        public static Usuario CrearUsuario( UsuarioDTO dto, string friendTag)
        {
            return new Usuario
            (
                dto.NombreUsuario,
                dto.CorreoUsuario,
                BCrypt.Net.BCrypt.HashPassword(dto.ContrasenaUsuario),
                dto.EstaEliminado == false,
                dto.FriendTag = friendTag
            );

        }
        public static void ModificarUsuario(Usuario usuario, EditarUsuarioDTO dto)
        {
            usuario.NombreUsuario = dto.NombreUsuario ?? usuario.NombreUsuario;
            usuario.CorreoUsuario = dto.CorreoUsuario ?? usuario.CorreoUsuario;
            usuario.ContrasenaUsuario = dto.ContrasenaUsuario != null
                ? BCrypt.Net.BCrypt.HashPassword(dto.ContrasenaUsuario)
                : usuario.ContrasenaUsuario;
            usuario.EstaEliminado = dto.EstaEliminado ?? usuario.EstaEliminado;
        }
    }
}
