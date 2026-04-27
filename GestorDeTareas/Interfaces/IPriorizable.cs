using GestorDeTareas.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Interfaces
{
    public interface IPriorizable
    {
        void PriorizarTarea(int id, CrearTareaUrgenteDTO dto);
        void QuitarPrioridadTarea(int id, CrearTareaDTO dto);
        void CrearTareaUrgente(CrearTareaUrgenteDTO dto);
    }
}
