using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
namespace GestorDeTareas
{
    public class TareaService<T> where T : Tarea
    {
        public readonly IRepositorio<T> _repository;

        public TareaService(IRepositorio<T> repository)
        {
            _repository = repository;
        }
        public Tarea MapearEdiccionTarea(Tarea tarea, EditarTareaDTO dto)
        {
            TareaMapper.ModificarEntidad(tarea, dto);
            return tarea;
        }
        public void CrearTarea(CrearTareaDTO dto)
        {
            var idUsuarioSesion = Sesion.IdUsuarioSesionActiva;
            var tarea = TareaMapper.CrearEntidad(dto);
            _repository.Guardar((T)tarea);
        }
        public void EditarTarea(int id, EditarTareaDTO dto)
        {
            var tareaFiltrada = _repository.ObtenerPorId(id);
            MapearEdiccionTarea(tareaFiltrada, dto);
            _repository.Guardar(tareaFiltrada);
        }
        public void EliminarTarea(int id)
        {
            var tareaFiltrada = _repository.ObtenerPorId(id);
            TareaMapper.EliminarEntidad(tareaFiltrada);
            _repository.Guardar(tareaFiltrada);
        }
    }
}
