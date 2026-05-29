using GestorDeTareas.DTOs;
using GestorDeTareas.Models;

namespace GestorDeTareas.Mapper
{
    public class MostrarDatosDeUsuarioLogueadoMapper
    {
        public static void MostrarDatosDeUsuarioLogueado(MostrarDatosUsuarioLogueadoDTO dto, Usuario usuario)
        {
            dto.NombreUsuario = usuario.NombreUsuario;
            dto.FriendTag = usuario.FriendTag;
            dto.IdUsuario = usuario.IdUsuario;
        }
    }
}
