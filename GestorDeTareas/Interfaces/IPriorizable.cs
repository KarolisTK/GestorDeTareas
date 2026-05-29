using GestorDeTareas.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Interfaces
{
    public interface IPriorizable
    {
        Task PriorizarTarea(int id, CrearTareaUrgenteDTO dto);
        Task QuitarPrioridadTarea(int id, CrearTareaDTO dto);
        Task CrearTareaUrgente(CrearTareaUrgenteDTO dto, int idUsuario);
    }
}
