using GestorDeTareas.DTOs;
using GestorDeTareas.Models;

namespace GestorDeTareas.Mapper
{
    public static class ObtenerAmigosPorFriendTagMapper
    {
        public static void ObtenerDatosDeAmigos (ObtenerDatosDeAmigoPorFriendTagDTO dto, Usuario usuario)
        {
            dto.NombreUsuario = usuario.NombreUsuario;
            dto.FriendTag = usuario.FriendTag;
        }
    }
}
