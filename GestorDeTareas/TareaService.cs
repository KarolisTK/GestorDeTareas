using GestorDeTareas.DTOs;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;

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
            var lista = repository.CargarListaDeUsuarios();
            var tareaFiltrada = FiltrarTareasParaTarea(lista, id);
            var tareaFiltradaEditada = MapearEdiccionTarea(tareaFiltrada, dto);
            repository.GuardarTarea(tareaFiltradaEditada);
        }
        public void EliminarTarea(int id)
        {
            var lista = repository.CargarListaDeUsuarios();
            var tareaFiltrada = FiltrarTareasParaTarea(lista, id);
            tareaFiltrada.EstaEliminado = true;
            repository.GuardarTarea(tareaFiltrada);
        }
        public Tarea FiltrarTareasParaTarea(List<Tarea> lista, int id)
        {
            return lista.FirstOrDefault(t => t.IdTarea == id);
        }
    }
}
