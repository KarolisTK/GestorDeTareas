using GestorDeTareas.Interfaces;

namespace GestorDeTareas.Models
{
    public class EspaciosDeTrabajo : IEntidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public bool EstaEliminado { get; set; }
    }
}
