using GestorDeTareas.Models;
using System.ComponentModel.DataAnnotations;

namespace GestorDeTareas.DTOs
{
    public class CrearNuevoEspacioDeTrabajoDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Nombre { get; set; }
    }
}
