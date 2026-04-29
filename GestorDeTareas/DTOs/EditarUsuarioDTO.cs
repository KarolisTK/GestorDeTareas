using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.DTOs
{
    public class EditarUsuarioDTO
    {
        public string? NombreUsuario { get; set; }
        public string? CorreoUsuario { get; set; }
        public string? ContrasenaUsuario { get; set; }
        public bool? EstaEliminado { get; set; }
    }
}
