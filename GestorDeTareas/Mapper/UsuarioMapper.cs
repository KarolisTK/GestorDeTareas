using GestorDeTareas.DTOs;
using GestorDeTareas.Models;
namespace GestorDeTareas.Mapper
{
    public class UsuarioMapper
    {
        public static Usuario CrearUsuario( UsuarioDTO dto)
        {
            var usuario = new Usuario();
            return usuario.CrearUsuarioConDTO(dto);
            
        }
        public static void ModificarUsuario(Usuario usuario, EditarUsuarioDTO dto)
        {
            usuario.EditarUsuarioConDTO(dto);
        }
    }
}
