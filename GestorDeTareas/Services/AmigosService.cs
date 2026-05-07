using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;

namespace GestorDeTareas.Services
{
    public class AmigosService
    {
        private readonly IUsuarioRepository _repository;

        public AmigosService(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public async Task<ObtenerDatosDeAmigoPorFriendTagDTO> BuscarAmigosPorFriendTag( string friendTag)
        {
            var usuario =await _repository.ObtenerPorFriendTag(friendTag);
            var dto = new ObtenerDatosDeAmigoPorFriendTagDTO();
            ObtenerAmigosPorFriendTagMapper.ObtenerDatosDeAmigos(dto, usuario);
            return dto;
        }
    }
}
