using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Exceptions;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GestorDeTareas.Services
{
    public class AmigosService : IAmigosService
    {
        private readonly IAmigosRepository _amigosRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public AmigosService(IAmigosRepository amigosRepository, IUsuarioRepository usuarioRepository)
        {
            _amigosRepository = amigosRepository;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<ObtenerDatosDeAmigoPorFriendTagDTO> BuscarAmigosPorFriendTag(string friendTag)
        {
            if (string.IsNullOrWhiteSpace(friendTag))
                throw new ConflictException("El FriendTag no puede estar vacío.");

            var usuario = await _usuarioRepository.ObtenerPorFriendTag(friendTag);
            if (usuario == null)
                throw new NotFoundException("No se encontró ningún usuario con ese FriendTag.");

            var dto = new ObtenerDatosDeAmigoPorFriendTagDTO();
            ObtenerAmigosPorFriendTagMapper.ObtenerDatosDeAmigos(dto, usuario);
            return dto;
        }

        public async Task AceptarSolicitudAmistad(int idUsuarioEmisor, int idUsuarioReceptor)
        {
            var envioCreado = new Amigos(idUsuarioEmisor, idUsuarioReceptor);
            await _amigosRepository.Guardar(envioCreado);
        }

        public async Task<List<ListarAmigosDTO>> ListarTodosLosAmigos(int idEmisor)
        {
            var amigos = await _amigosRepository.ObtenerAmigosDeUsuario(idEmisor);
            return amigos.Select(a => new ListarAmigosDTO
            {
                IdAmigo = a.IdEmisor == idEmisor ? a.IdReceptor : a.IdEmisor,
                NombreAmigo = a.IdEmisor == idEmisor
                    ? a.Receptor.NombreUsuario
                    : a.Emisor.NombreUsuario,
                IdAmigoLogueado = idEmisor
            }).ToList();
        }

        public async Task<List<SolicitudAmistadDto>> ListarSolicitudesDeAmistad(int id)
        {
            return await _amigosRepository.ObtenerSolicitudesPendientes(id);
        }
    }
}
