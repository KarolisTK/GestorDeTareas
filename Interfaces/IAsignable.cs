using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Interfaces
{
    public interface IAsignable
    {
        bool EstaAsignada { get; }
        void AsignarTarea(int idTarea, string nombreUsuario);
        void quitarAsignacionTarea(int idTarea);

    }
}
