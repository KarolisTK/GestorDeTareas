using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GestorDeTareas.Models
{
    public class TareaUrgente : Tarea
    {
        public TareaUrgente() { }
        public TareaUrgente(string nombreTarea, string descripcionTarea, DateTime fechaCreacion, EstadosTarea? estadosTarea, bool? estaEliminado, TiposTarea? tiposTarea, int idUsuario, bool? tienePrioridad, DateTime fechaLimite) : base(nombreTarea, descripcionTarea, fechaCreacion, estadosTarea, estaEliminado, tiposTarea, idUsuario)
        {
            TienePrioridad = tienePrioridad;
            FechaLimite = fechaLimite;
        }

        public bool? TienePrioridad { get; set; }
        public DateTime? FechaLimite { get; set; }
    }
}
