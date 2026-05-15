using GestorDeTareas.DTOs;
using GestorDeTareas.Exceptions;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
using GestorDeTareas.Repositories;
namespace GestorDeTareas.Services
{
    public class UsuarioService
    {
        private readonly IRepositorio<Usuario> _repository;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IRepositorio<Usuario> repository, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<Usuario> ObtenerUnUsuarioPorID(int IdUsuario)
        {
            return await _repository.ObtenerPorId(IdUsuario);
        }

        public async Task<Usuario> ObtenerUsuarioPorCorreo(string correo)
        {
            return await _usuarioRepository.ObtenerPorCorreo(correo);
        }
        public async Task CrearUsuario(UsuarioDTO dto)
        {
            var usuarios = await _repository.ObtenerTodos();
            if (usuarios.Any(u => u.CorreoUsuario == dto.CorreoUsuario))
            {
                throw new ConflictException("El correo ya está en uso");
            }
            var friendTag = dto.NombreUsuario[..3] + Random.Shared.Next(10000, 99999);
            if(usuarios.Any(u => u.FriendTag == friendTag))
            {
                friendTag = friendTag + Random.Shared.Next(10000, 99999);
            }
            var usuario = UsuarioMapper.CrearUsuario(dto,friendTag);
            await _repository.Guardar(usuario);
        }
        public async Task EditarUsuario(EditarUsuarioDTO dto, int IdUsuario)
        {
            if(dto == null)
            {
                throw new ForbiddenException("los datos para editar usuario han llegado nulos");
            }
            var usuarioFiltrado = await _repository.ObtenerPorId(IdUsuario);
            if (usuarioFiltrado == null) 
            {
                throw new ForbiddenException("El usuario filtrado para editar usuario no existe");
            }
            UsuarioMapper.ModificarUsuario(usuarioFiltrado, dto);
            await _repository.Guardar(usuarioFiltrado);
        }
        public async Task EliminarUsuario(int idUsuario)
        {
            var usuarioFiltrado = await _repository.ObtenerPorId(idUsuario);
            if (usuarioFiltrado == null)
            {
                throw new ForbiddenException("El usuario filtrado para eliminar usuario no existe");
            }
            if (usuarioFiltrado.EstaEliminado == true)
            {
                throw new ConflictException("El usuario filtrado ya está eliminado");
            }
            usuarioFiltrado.EstaEliminado = true;
            await _repository.Guardar(usuarioFiltrado);
        }
    }
}
