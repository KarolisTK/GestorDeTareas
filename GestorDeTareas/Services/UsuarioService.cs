using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
namespace GestorDeTareas.Services
{
    public class UsuarioService
    {
        private readonly IRepositorio<Usuario> _repository;

        public UsuarioService(IRepositorio<Usuario> repository)
        {
            _repository = repository;
        }

        public Usuario MapearUsuario(UsuarioDTO dto)
        {
            return UsuarioMapper.CrearUsuario(dto);
        }
        public Usuario MapearEdiccionUsuario(Usuario usuario, EditarUsuarioDTO dto)
        {
            UsuarioMapper.ModificarUsuario(usuario, dto);
            return usuario;
        }
        public void CrearUsuario(UsuarioDTO dto)
        {
            var usuario = MapearUsuario(dto);
            _repository.Guardar(usuario);
        }
        public void EditarUsuario(EditarUsuarioDTO dto, int IdUsuario)
        {
            var usuarioFiltrado = _repository.ObtenerPorId(IdUsuario);
            var usuarioEditado = MapearEdiccionUsuario(usuarioFiltrado, dto);
            _repository.Guardar(usuarioEditado);
        }
        public void EliminarUsuario(int idUsuario)
        {
            var usuarioFiltrado = _repository.ObtenerPorId(idUsuario);
            usuarioFiltrado.EstaEliminado = true;
            _repository.Guardar(usuarioFiltrado);
        }
    }
}
