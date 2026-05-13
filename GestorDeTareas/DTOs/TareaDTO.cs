using GestorDeTareas.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestorDeTareas.DTOs
{
    public class TareaDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string NombreTarea { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 3)]
        public string DescripcionTarea { get; set; }
        public DateTime FechaCreacionTarea { get; set; }
        public EstadosTarea? EstadosTarea { get; set; }
        public bool? EstaEliminado { get; set; }
        public TiposTarea? TiposTarea {  get; set; }
        [Range(0, double.MaxValue)]
        public int IdUsuarioDeLaTarea { get; set; }
        public int IdEspacioDeTrabajo { get; set; }
    }
}
