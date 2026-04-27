using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Interfaces
{
    public interface IService<T>
    {
        void Crear(object dto);
        void Editar(int id, object dto);
        void Eliminar(int id);
    }
}
