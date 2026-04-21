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
            var id = Guid.NewGuid().ToString();
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
        public void EditarUsuario(string id, EditarUsuarioDTO dto)
        {
            var lista = repository.CargarListaEnJson();
            var usuarioFiltrado = FiltrarUsuariosPorUsuario(lista, id);
            MapearEdiccionUsuario(usuarioFiltrado, dto);
            repository.GuardarListaEnJson(lista);
        }
        public void EliminarUsuario(string id)
        {
            var lista = repository.CargarListaEnJson();
            var usuarioFiltrado = FiltrarUsuariosPorUsuario(lista, id);
            usuarioFiltrado.EstaEliminado = true;
            repository.GuardarListaEnJson(lista);
        }

        public void SacarUsuariosPorPantalla()
        {
            var listaDeUsuarios = repository.CargarListaEnJson();
            foreach (var usuario in listaDeUsuarios)
            {
                Console.WriteLine(usuario.Name + " " + usuario.Email);
            }
        }

        public Usuario FiltrarUsuariosPorUsuario(List<Usuario> usuario, string id)
        {
            return usuario.FirstOrDefault(t => t.IdUsuario == id);
        }

        public void MostrarTarea(string id)
        {
            var usuario = repository.CargarSoloUnUsuarioPorID(id);

            Console.WriteLine("================================");
            Console.WriteLine($"  Nombre:      {usuario.Name}");
            Console.WriteLine($"  Email: {usuario.Email}");
            Console.WriteLine($"  Contraseña:      {usuario.Password.GetHashCode()}");
            Console.WriteLine($"  Eliminada:   {(usuario.EstaEliminado.GetValueOrDefault() ? "Sí" : "No")}");
            Console.WriteLine("================================");
        }
    }
}
