using GestorDeTareas.DTOs;
using GestorDeTareas.Models;
namespace GestorDeTareas.Mapper
{
    public class UsuarioMapper
    {
        public static Usuario CrearUsuario( UsuarioDTO dto)
        {
            return new Usuario
            (
                dto.NombreUsuario,
                dto.CorreoUsuario,
                dto.ContrasenaUsuario,
                dto.EstaEliminado = false
            );

        }
        public static void ModificarUsuario(Usuario usuario, EditarUsuarioDTO dto)
        {
            usuario.NombreUsuario = dto.NombreUsuario ?? usuario.NombreUsuario;
            usuario.CorreoUsuario = dto.CorreoUsuario ?? usuario.CorreoUsuario;
            usuario.ContrasenaUsuario = dto.ContrasenaUsuario ?? usuario.ContrasenaUsuario;
            usuario.EstaEliminado = dto.EstaEliminado ?? usuario.EstaEliminado;
        }
    }
}
