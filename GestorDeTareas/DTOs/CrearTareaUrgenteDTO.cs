using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.DTOs
{
    public class CrearTareaUrgenteDTO : TareaDTO
    {
        public DateTime FechaLimite { get; set; }
        public bool TienePrioridad { get; set; }
    }
}
