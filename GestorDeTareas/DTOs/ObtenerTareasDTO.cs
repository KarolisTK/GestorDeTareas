using GestorDeTareas.Enums;
using GestorDeTareas.Models;

namespace GestorDeTareas.DTOs
{
    public class ObtenerTareasDTO
    {
        public int IdTarea { get; set; }
        public string? NombreTarea { get; set; }
        public string? DescripcionTarea { get; set; }
        public DateTime? FechaCreacionTarea { get; set; }
        public DateTime? FechaLimite { get; set; }
        public EstadosTarea? EstadosTarea { get; set; }
        public bool? EstaEliminado { get; set; }
        public bool? TienePrioridad { get; set; }
        public TiposTarea? TiposTarea { get; set; }
        public int IdUsuarioDeLaTarea { get; set; }
        public int EspacioDeTrabajoId { get; set; }
    }
}
