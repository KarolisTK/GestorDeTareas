using GestorDeTareas.DTOs;
using GestorDeTareas.Exceptions;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
using GestorDeTareas.Repositories;
namespace GestorDeTareas.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        public async Task<Usuario> ObtenerUnUsuarioPorID(int IdUsuario)
        {
            return await _usuarioRepository.ObtenerPorId(IdUsuario);
        }

        public async Task<Usuario> ObtenerUsuarioPorCorreo(string correo)
        {
            return await _usuarioRepository.ObtenerPorCorreo(correo);
        }
        public async Task CrearUsuario(CrearUsuarioDTO dto)
        {
            var usuarioExistente = await _usuarioRepository.ObtenerPorCorreo(dto.CorreoUsuario);
            if (usuarioExistente != null)
                throw new ConflictException("El correo ya está en uso");
            if (dto.ContrasenaUsuario.Length < 15) {
                throw new PasswordException("la contraseña tiene que ser de 15 carácteres como mínimo");
            }

            var friendTag = dto.NombreUsuario[..3] + Random.Shared.Next(10000, 99999);
            var friendTagExistente = await _usuarioRepository.ObtenerPorFriendTag(friendTag);
            if (friendTagExistente != null)
                friendTag = friendTag + Random.Shared.Next(10000, 99999);

            var usuario = UsuarioMapper.CrearUsuario(dto, friendTag);
            await _usuarioRepository.Guardar(usuario);
        }
        public async Task EditarUsuario(EditarUsuarioDTO dto, int IdUsuario)
        {
            if(dto == null)
            {
                throw new NotFoundException("los datos para editar usuario han llegado nulos");
            }
            var usuarioFiltrado = await _usuarioRepository.ObtenerPorId(IdUsuario);
            if (usuarioFiltrado == null) 
            {
                throw new NotFoundException("El usuario filtrado para editar usuario no existe");
            }
            UsuarioMapper.ModificarUsuario(usuarioFiltrado, dto);
            await _usuarioRepository.Guardar(usuarioFiltrado);
        }
        public async Task EliminarUsuario(int idUsuario)
        {
            var usuarioFiltrado = await _usuarioRepository.ObtenerPorId(idUsuario);
            if (usuarioFiltrado == null)
            {
                throw new ForbiddenException("El usuario filtrado para eliminar usuario no existe");
            }
            if (usuarioFiltrado.EstaEliminado == true)
            {
                throw new ConflictException("El usuario filtrado ya está eliminado");
            }
            usuarioFiltrado.EstaEliminado = true;
            await _usuarioRepository.Guardar(usuarioFiltrado);
        }
    }
}
