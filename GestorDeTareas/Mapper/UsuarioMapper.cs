using GestorDeTareas.DTOs;
using GestorDeTareas.Models;
namespace GestorDeTareas.Mapper
{
    public class UsuarioMapper
    {
        public static Usuario CrearUsuario( UsuarioDTO dto)
        {
            return new Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                CorreoUsuario = dto.CorreoUsuario,
                ContrasenaUsuario = dto.ContrasenaUsuario,
                EstaEliminado = false,

            };

        }
        public static void ModificarUsuario(Usuario usuario, EditarUsuarioDTO dto)
        {
            if (dto.NombreUsuario != null)
                usuario.NombreUsuario = dto.NombreUsuario;

            if (dto.CorreoUsuario != null)
                usuario.CorreoUsuario = dto.CorreoUsuario;

            if (dto.ContrasenaUsuario != null)
                usuario.ContrasenaUsuario = dto.ContrasenaUsuario;

            if (dto.EstaEliminado != null)
                usuario.EstaEliminado = dto.EstaEliminado;
        }
    }
}
