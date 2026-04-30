using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Interfaces
{
    public interface IRepositorio<T>
    {
        Task<List<T>> ObtenerTodos();
        Task<T> ObtenerPorId(int id);
        Task Guardar(T entidad);
    }
}
