using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Interfaces
{
    public interface ICompletable
    {
        bool EstaCompleta { get; }
        void CompletarTarea(int idtarea);
    }
}
