using GestorDeTareas.DTOs;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
            repository.GuardarLista(usuario);
        }
        public void EditarUsuario(EditarUsuarioDTO dto)
        {
            var lista = repository.CargarListaDeUsuarios();
            var usuarioFiltrado = FiltrarUsuariosPorUsuario(lista, Sesion.IdUsuarioSesionActiva);
            var usuarioEditado = MapearEdiccionUsuario(usuarioFiltrado, dto);
            repository.GuardarLista(usuarioEditado);
        }
        public void EliminarUsuario()
        {
            var lista = repository.CargarListaDeUsuarios();
            var usuarioFiltrado = FiltrarUsuariosPorUsuario(lista, Sesion.IdUsuarioSesionActiva);
            usuarioFiltrado.EstaEliminado = true;
            repository.GuardarLista(usuarioFiltrado);
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

        public void SacarUsuariosPorPantalla()
        {
            var listaDeUsuarios = repository.CargarListaDeUsuarios();
            foreach (var usuario in listaDeUsuarios)
            {
                Console.WriteLine(usuario.NombreUsuario + " " + usuario.CorreoUsuario);
            }
        }

        public Usuario FiltrarUsuariosPorUsuario(List<Usuario> usuario, int id)
        {
            return usuario.FirstOrDefault(t => t.IdUsuario == id);
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
