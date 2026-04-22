using GestorDeTareas.DTOs;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
namespace GestorDeTareas
{
    public class UsuarioService
    {
        UsuarioRepository repository = new UsuarioRepository();
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
            repository.GuardarTarea(usuario);
        }
        public void EditarUsuario(EditarUsuarioDTO dto)
        {
            var usuarioFiltrado = repository.CargarSoloUnUsuarioPorID(Sesion.IdUsuarioSesionActiva);
            var usuarioEditado = MapearEdiccionUsuario(usuarioFiltrado, dto);
            repository.GuardarTarea(usuarioEditado);
        }
        public void EliminarUsuario()
        {
            var usuarioFiltrado = repository.CargarSoloUnUsuarioPorID(Sesion.IdUsuarioSesionActiva);
            usuarioFiltrado.MarcarUsuarioComoEliminado();
            repository.GuardarTarea(usuarioFiltrado);
        }

        public void IniciarSesion(string CorreoUsuario, string ContrasenaUsuario)
        {
            var lista = repository.CargarListaDeUsuarios();
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
