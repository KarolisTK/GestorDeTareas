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

        public List<Usuario> CargarListaDeUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario CargarSoloUnUsuarioPorID(int id)
        {
            var Usuario = CargarListaDeUsuarios();
            var UsuarioFiltrado = Usuario.Where(u => u.IdUsuario == id).FirstOrDefault();
            return UsuarioFiltrado;
        }

        public void GuardarTarea(Usuario usuario)
        {
            if(usuario.IdUsuario != 0)
            {
                _context.Usuarios.Update(usuario);
                _context.SaveChanges();
            }
            else
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
            }
            
        }
    }
}
