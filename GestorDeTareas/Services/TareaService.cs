using GestorDeTareas.DTOs;
using GestorDeTareas.Exceptions;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
using GestorDeTareas.Repositories;
using System.Security.Claims;
namespace GestorDeTareas
{
    public class TareaService : ITareaService
    {
        private readonly ITareasRepository _tareasRepository;

        public TareaService(ITareasRepository tareasRepository)
        {
            _tareasRepository = tareasRepository;
        }

        public async Task< List<ObtenerTareasDTO>> ObtenerTodas(int idEspacioDeTrabajo, int idUsuario)
        {
           var tareas = await _tareasRepository.ObtenerPorEspacioYUsuario(idEspacioDeTrabajo,idUsuario);
            return tareas;
        }
        public async Task< ObtenerTareasDTO> ObtenerUnaTareaPorID(int idTarea)
        {
            var tareaFiltrada = await _tareasRepository.ObtenerPorId(idTarea);
            if(tareaFiltrada == null)
            {
                throw new NotFoundException("La tarea filtrada no existe");
            }
            var tarea = ObtenerTareaPorIdTareaMapper.Map(tareaFiltrada);
            return tarea;
        }
        public async Task CrearTarea(TareaDTO dto, int idUsuario)
        {
            var tareas = await _tareasRepository.ObtenerTodos();
            var tareaExistente = tareas.Any(t => t.NombreTarea == dto.NombreTarea && t.IdUsuarioDeLaTarea == idUsuario);
            if (tareaExistente)
            {
                throw new ConflictException("Esa tarea ya existe");
            }
            var tarea = TareaMapper.CrearEntidad(dto, idUsuario);
            await _tareasRepository.Guardar(tarea);
        }
        public async Task EditarTarea(int id, EditarTareaDTO dto, int idUsuario)
        {
            
            if (dto == null)
            {
                throw new ConflictException("El dto ha llegado nulo, no hay nada que editar.");
            }

            var tareaFiltrada = await _tareasRepository.ObtenerPorId(id);
            if(tareaFiltrada == null)
            {
                throw new NotFoundException("La tarea filtrada no existe");
            }
            if (tareaFiltrada.IdUsuarioDeLaTarea != idUsuario)
            {
                throw new ForbiddenException("La tarea que se está intentado editar no pertenece al usuario Logueado");
            }
            TareaMapper.ModificarEntidad(tareaFiltrada, dto);
            await _tareasRepository.Guardar(tareaFiltrada);
        }
        public async Task EliminarTarea(int id)
        {
            var tareaFiltrada = await _tareasRepository.ObtenerPorId(id);
            if (tareaFiltrada == null)
            {
                throw new NotFoundException("La tarea filtrada no existe");
            }
            if(tareaFiltrada.EstaEliminado == true){
                throw new ConflictException("La tarea filtrada ya está eliminada");
            }
            TareaMapper.EliminarEntidad(tareaFiltrada);
            await _tareasRepository.Guardar(tareaFiltrada);
        }
    }
}
