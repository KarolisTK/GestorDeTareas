using GestorDeTareas.Models;

namespace GestorDeTareas.DTOs
{
    public class CrearNuevoEspacioDeTrabajoDTO
    {
        public string Nombre { get; set; }
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
