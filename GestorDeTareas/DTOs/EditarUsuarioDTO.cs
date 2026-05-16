using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestorDeTareas.DTOs
{
    public class EditarUsuarioDTO
    {
        [StringLength(100, MinimumLength = 3)]
        public string? NombreUsuario { get; set; }

        [EmailAddress]
        [StringLength(100, MinimumLength = 5)]
        public string? CorreoUsuario { get; set; }

        [StringLength(100, MinimumLength = 8)]
        public string? ContrasenaUsuario { get; set; }
        public bool? EstaEliminado { get; set; }
    }
}
