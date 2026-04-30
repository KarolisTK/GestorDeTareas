using System.ComponentModel.DataAnnotations;

namespace GestorDeTareas.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Correo { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Contrasena { get; set; }
    }
}
