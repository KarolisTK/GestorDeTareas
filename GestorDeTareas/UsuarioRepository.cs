using GestorDeTareas.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GestorDeTareas
{
    public class UsuarioRepository
    {
        private readonly AppDbContext _context = new AppDbContext();
        private readonly string _Ruta;

        public List<Usuario> CargarListaDeUsuarios()
        {
            return _context.Usuario.ToList();
        }

        public Usuario CargarSoloUnUsuarioPorID(int id)
        {
            var Usuario = CargarListaDeUsuarios();
            var UsuarioFiltrado = Usuario.Where(u => u.IdUsuario == id).FirstOrDefault();
            return UsuarioFiltrado;
        }

        public void GuardarLista(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }
    }
}
