using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace GestorDeTareas.DTOs
{
    public class UsuarioDTO
    {
        public string IdUsuario { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Tarea>? Tareas {  get; set; }
        public bool? EstaEliminado {  get; set; }
    }
}
