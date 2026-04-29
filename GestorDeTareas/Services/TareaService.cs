using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
namespace GestorDeTareas
{
    public class TareaService
    {
        public readonly IRepositorio<Tarea> _repository;

        public TareaService(IRepositorio<Tarea> repository)
        {
            _repository = repository;
        }
        public Tarea MapearEdiccionTarea(Tarea tarea, EditarTareaDTO dto)
        {
            TareaMapper.ModificarEntidad(tarea, dto);
            return tarea;
        }

        public List<Tarea> ObtenerTodas()
        {
           return _repository.ObtenerTodos();

        }

        public Tarea ObtenerUnaTareaPorID(int idTarea)
        {
            return _repository.ObtenerPorId(idTarea);
        }
        public void CrearTarea(CrearTareaDTO dto)
        {
            var idUsuarioSesion = Sesion.IdUsuarioSesionActiva;
            var tarea = TareaMapper.CrearEntidad(dto);
            _repository.Guardar(tarea);
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
