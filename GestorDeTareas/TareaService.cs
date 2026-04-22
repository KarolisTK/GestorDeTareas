using GestorDeTareas.DTOs;
using GestorDeTareas.Mapper;
namespace GestorDeTareas
{
    public class TareaService
    {
        TareaRepository repository = new TareaRepository();

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
            repository.GuardarTarea(tarea);
        }
        public void EditarTarea(int id, EditarTareaDTO dto)
        {
            var tareaFiltrada = repository.CargarSoloUnaTareaPorID(id);
            var tareaFiltradaEditada = MapearEdiccionTarea(tareaFiltrada, dto);
            repository.GuardarTarea(tareaFiltradaEditada);
        }
        public void EliminarTarea(int id)
        {
            var tareaFiltrada = repository.CargarSoloUnaTareaPorID(id);
            TareaMapper.EliminarEntidad(tareaFiltrada);
            repository.GuardarTarea(tareaFiltrada);
        }
    }
}
