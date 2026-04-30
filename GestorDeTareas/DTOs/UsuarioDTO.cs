using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace GestorDeTareas.DTOs
{
    public class UsuarioDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string CorreoUsuario { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 15)]
        public string ContrasenaUsuario { get; set; }
        public bool? EstaEliminado {  get; set; }
    }
}
