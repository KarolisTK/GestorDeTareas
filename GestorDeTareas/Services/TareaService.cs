using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
using System.Security.Claims;
namespace GestorDeTareas
{
    public class TareaService
    {
        public readonly IRepositorio<Tarea> _repository;

        public TareaService(IRepositorio<Tarea> repository)
        {
            _repository = repository;
        }

        public async Task< List<Tarea>> ObtenerTodas(int idUsuario)
        {
           var tareas = await _repository.ObtenerTodos();
           return tareas.Where(t => t.IdUsuarioDeLaTarea == idUsuario).ToList();
        }
        public async Task< Tarea> ObtenerUnaTareaPorID(int idTarea)
        {
            var tareaFiltrada = await _repository.ObtenerPorId(idTarea);
            if(tareaFiltrada == null)
            {
                throw new Exception("La tarea filtrada no existe");
            }
            return tareaFiltrada;
        }
        public async Task CrearTarea(TareaDTO dto, int idUsuario)
        {
            var tareas = await _repository.ObtenerTodos();
            var tareaExistente = tareas.Any(t => t.NombreTarea == dto.NombreTarea && t.IdUsuarioDeLaTarea == idUsuario);
            if (tareaExistente)
            {
                throw new Exception("Esa tarea ya existe");
            }
            var tarea = TareaMapper.CrearEntidad(dto, idUsuario);
            await _repository.Guardar(tarea);
        }
        public async Task EditarTarea(int id, EditarTareaDTO dto, int idUsuario)
        {
            
            if (dto == null)
            {
                throw new Exception("El dto ha llegado nulo, no hay nada que editar.");
            }

            var tareaFiltrada = await _repository.ObtenerPorId(id);
            if(tareaFiltrada == null)
            {
                throw new Exception("La tarea filtrada no existe");
            }
            if (tareaFiltrada.IdUsuarioDeLaTarea != idUsuario)
            {
                throw new Exception("La tarea que se está intentado editar no pertenece al usuario Logueado");
            }
            TareaMapper.ModificarEntidad(tareaFiltrada, dto);
            await _repository.Guardar(tareaFiltrada);
        }
        public async Task EliminarTarea(int id)
        {
            var tareaFiltrada = await _repository.ObtenerPorId(id);
            if (tareaFiltrada == null)
            {
                throw new Exception("La tarea filtrada no existe");
            }
            if(tareaFiltrada.EstaEliminado == true){
                throw new Exception("La tarea filtrada ya está eliminada");
            }
            TareaMapper.EliminarEntidad(tareaFiltrada);
            await _repository.Guardar(tareaFiltrada);
        }
    }
}
