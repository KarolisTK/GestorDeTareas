using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Interfaces
{
    public interface IRepositorio<T>
    {
        List<T> ObtenerTodos();
        T ObtenerPorId(int id);
        void Guardar(T entidad);
    }
}
