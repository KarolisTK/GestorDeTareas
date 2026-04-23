using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
namespace GestorDeTareas
{
    public class TareaService
    {
        private readonly IRepositorio<Tarea> _repository;

        public TareaService(IRepositorio<Tarea> repository)
        {
            _repository = repository;
        }

        public Tarea MapearTarea(CrearTareaDTO dto)
        {
            return TareaMapper.CrearEntidad(dto);
        }
        public Tarea MapearEdiccionTarea(Tarea tarea, EditarTareaDTO dto)
        {
            TareaMapper.ModificarEntidad(tarea, dto);
            return tarea;
        }
        public void CrearTarea(CrearTareaDTO dto)
        {
            var idUsuarioSesion = Sesion.IdUsuarioSesionActiva;
            var tarea = MapearTarea(dto);
            _repository.Guardar(tarea);
        }
        public void EditarTarea(int id, EditarTareaDTO dto)
        {
            var tareaFiltrada = _repository.ObtenerPorId(id);
            var tareaFiltradaEditada = MapearEdiccionTarea(tareaFiltrada, dto);
            _repository.Guardar(tareaFiltradaEditada);
        }
        public void EliminarTarea(int id)
        {
            var tareaFiltrada = _repository.ObtenerPorId(id);
            TareaMapper.EliminarEntidad(tareaFiltrada);
            _repository.Guardar(tareaFiltrada);
        }
    }
}
