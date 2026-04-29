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
        public void EditarUsuario(EditarUsuarioDTO dto)
        {
            var usuarioFiltrado = _repository.ObtenerPorId(Sesion.IdUsuarioSesionActiva);
            var usuarioEditado = MapearEdiccionUsuario(usuarioFiltrado, dto);
            _repository.Guardar(usuarioEditado);
        }
        public void EliminarUsuario()
        {
            var usuarioFiltrado = _repository.ObtenerPorId(Sesion.IdUsuarioSesionActiva);
            usuarioFiltrado.EstaEliminado = true;
            _repository.Guardar(usuarioFiltrado);
        }

        public void IniciarSesion(string CorreoUsuario, string ContrasenaUsuario)
        {
            var lista = _repository.ObtenerTodos();
            var usuarioFiltrado = FiltrarUsuariosPorEmailYContrasena(lista, CorreoUsuario, ContrasenaUsuario);
            if (usuarioFiltrado != null)
            {
                Sesion.IdUsuarioSesionActiva = usuarioFiltrado.IdUsuario;
            }

        }
        public Usuario FiltrarUsuariosPorEmailYContrasena(List<Usuario> usuario, string CorreoUsuario, string contrasena)
        {
            if(CorreoUsuario != null && contrasena != null)
            {
                return usuario.FirstOrDefault(t => t.CorreoUsuario == CorreoUsuario && t.ContrasenaUsuario == contrasena);
            }
            return null;
           
        }
    }
}
