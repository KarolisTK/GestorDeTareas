using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.DTOs
{
    public class EditarUsuarioDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? EstaEliminado { get; set; }
    }
}
