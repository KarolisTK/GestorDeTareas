using GestorDeTareas.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.DTOs
{
    public class EditarTareaDTO
    {
        public string NombreTarea { get; set; }
        public string DescripcionTarea { get; set; }
        public EstadosTarea? EstadosTarea { get; set; }
        public bool? EstaEliminado { get; set; }
        public TiposTarea? TiposTarea { get; set; }
    }
}
