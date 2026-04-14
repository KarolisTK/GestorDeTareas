using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Interfaces
{
    public interface IPriorizable
    {
        bool tienePrioridad { get; }
        void PriorizarTarea(int idTarea);

        void quitarPrioridadTarea(int idTarea);
    }
}
