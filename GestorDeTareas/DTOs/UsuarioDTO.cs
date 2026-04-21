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
        public string NombreUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string ContrasenaUsuario { get; set; }
        public List<Tarea>? Tareas {  get; set; }
        public bool? EstaEliminado {  get; set; }
    }
}
