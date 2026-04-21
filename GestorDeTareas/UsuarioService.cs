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
            var id = new Random().Next();
            return UsuarioMapper.CrearUsuario(id, dto);
        }
        public Usuario MapearEdiccionUsuario(Usuario usuario, EditarUsuarioDTO dto)
        {
            UsuarioMapper.ModificarUsuario(usuario, dto);
            return usuario;
        }
        public void CrearUsuario(UsuarioDTO dto)
        {
            var lista = repository.CargarListaEnJson();
            var usuario = MapearUsuario(dto);
            lista.Add(usuario);
            repository.GuardarListaEnJson(lista);
        }
        public void EditarUsuario(int id, EditarUsuarioDTO dto)
        {
            var lista = repository.CargarListaEnJson();
            var usuarioFiltrado = FiltrarUsuariosPorUsuario(lista, id);
            MapearEdiccionUsuario(usuarioFiltrado, dto);
            repository.GuardarListaEnJson(lista);
        }
        public void EliminarUsuario(int id)
        {
            var lista = repository.CargarListaEnJson();
            var usuarioFiltrado = FiltrarUsuariosPorUsuario(lista, id);
            usuarioFiltrado.EstaEliminado = true;
            repository.GuardarListaEnJson(lista);
        }

        public void IniciarSesion(string CorreoUsuario, string ContrasenaUsuario)
        {
            var lista = repository.CargarListaEnJson();
            var usuarioFiltrado = FiltrarUsuariosPorEmailYContrasena(lista, CorreoUsuario, ContrasenaUsuario);
            if (usuarioFiltrado != null)
            {
                Sesion.IdUsuarioSesionActiva = usuarioFiltrado.IdUsuario;
            }

        }

        public void SacarUsuariosPorPantalla()
        {
            var listaDeUsuarios = repository.CargarListaEnJson();
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

        public void MostrarTarea(int id)
        {
            var usuario = repository.CargarSoloUnUsuarioPorID(id);

            Console.WriteLine("================================");
            Console.WriteLine($"  Nombre:      {usuario.NombreUsuario}");
            Console.WriteLine($"  CorreoUsuario: {usuario.CorreoUsuario}");
            Console.WriteLine($"  Contraseña:      {usuario.ContrasenaUsuario.GetHashCode()}");
            Console.WriteLine($"  Eliminada:   {(usuario.EstaEliminado.GetValueOrDefault() ? "Sí" : "No")}");
            Console.WriteLine("================================");
        }
    }
}
