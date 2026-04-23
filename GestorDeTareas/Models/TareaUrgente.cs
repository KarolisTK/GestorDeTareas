using GestorDeTareas.DTOs;
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
        public bool TienePrioridad { get; set; }
        public DateTime? FechaLimite { get; set; }
    }
}
