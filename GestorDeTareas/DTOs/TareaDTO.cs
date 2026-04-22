using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.DTOs
{
    public class CrearTareaDTO
    {
        public string NombreTarea { get; set; }
        public string DescripcionTarea { get; set; }
        public DateTime FechaCreacionTarea { get; set; }
        public EstadosTarea? EstadosTarea { get; set; }
        public bool? EstaEliminado { get; set; }
        public TiposTarea? TiposTarea {  get; set; }
        public int IdUsuarioDeLaTarea { get; set; }
    }
}
