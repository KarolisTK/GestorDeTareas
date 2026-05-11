using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GestorDeTareas.Services
{
    public class AmigosService
    {
        private readonly IAmigosRepository _amigosRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public AmigosService(IAmigosRepository amigosRepository, IUsuarioRepository usuarioRepository)
        {
            _amigosRepository = amigosRepository;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<ObtenerDatosDeAmigoPorFriendTagDTO> BuscarAmigosPorFriendTag( string friendTag)
        {
            var usuario =await _usuarioRepository.ObtenerPorFriendTag(friendTag);
            var dto = new ObtenerDatosDeAmigoPorFriendTagDTO();
            ObtenerAmigosPorFriendTagMapper.ObtenerDatosDeAmigos(dto, usuario);
            return dto;
        }

        public async Task EnviarSolicitudAmistad(int idUsuarioEmisor, int idUsuarioReceptor)
        {
            var envioCreado = new Amigos(idUsuarioEmisor, idUsuarioReceptor);
            await _amigosRepository.Guardar(envioCreado);
        }

        public async Task TramitarSolicitudAmistad(int idSolicitud, TiposEstadoAmistad resolucionSolicitudAmistad, int idUsuario)
        {
            var solicitud = await _amigosRepository.ObtenerPorId(idSolicitud);
            if (solicitud is null)
                throw new KeyNotFoundException($"No existe una solicitud con id {idSolicitud}");
            if (solicitud.IdUsuario2 != idUsuario)
                throw new UnauthorizedAccessException("No tienes permiso para tramitar esta solicitud.");
            if (solicitud.TiposEstado != TiposEstadoAmistad.Pendiente)
                throw new InvalidOperationException("Esta solicitud ya fue tramitada.");

            solicitud.TiposEstado = resolucionSolicitudAmistad;
            await _amigosRepository.Guardar(solicitud);
        }

        public async Task<List<ListarAmigosDTO>> ListarTodosLosAmigos(int idUsuario)
        {
            var amigos = await _amigosRepository.ObtenerAmigosDeUsuario(idUsuario);
            return amigos.Select(a => new ListarAmigosDTO
            {
                IdAmigo = a.IdUsuario == idUsuario ? a.IdUsuario2 : a.IdUsuario,
                NombreAmigo = a.IdUsuario == idUsuario
                    ? a.Usuario2.NombreUsuario
                    : a.Usuario.NombreUsuario,
                IdAmigoLogueado = idUsuario
            }).ToList();
        }

        public async Task<List<SolicitudAmistadDto>> ListarSolicitudesDeAmistad(int id)
        {
            return await _amigosRepository.ObtenerSolicitudesPendientes(id);
        }
    }
}
