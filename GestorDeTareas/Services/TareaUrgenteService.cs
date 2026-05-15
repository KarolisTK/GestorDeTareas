using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Services
{
    public class TareaUrgenteService : TareaService, IPriorizable
    {
        private readonly IRepositorio<Tarea> _repositoryBase;
        private readonly IRepositorio<TareaUrgente> _repository;
        public TareaUrgenteService(
            IRepositorio<TareaUrgente> repository,
            IRepositorio<Tarea> repositorioBase, ITareasRepository tareasRepository)
            : base(repositorioBase, tareasRepository)
        {
            _repositoryBase = repositorioBase;
            _repository = repository;
        }
        public async Task PriorizarTarea(int id, CrearTareaUrgenteDTO dto)
        {
            if (dto == null)
            {
                throw new Exception("Los datos para priorizar la tarea han llegado nulos");
            }
            var tareaOriginal = await _repositoryBase.ObtenerPorId(id);
            if (tareaOriginal == null)
            {
                throw new Exception("La tarea que se está intentando priorizar no existe");
            }
            tareaOriginal.EstaEliminado = true;
            await _repositoryBase.Guardar(tareaOriginal);

            var tareaUrgente = new TareaUrgente();
            var tareaUrgenteModificada = TareaUrgenteMapper.ModificarEntidad(tareaUrgente, dto, tareaOriginal);
            await _repository.Guardar(tareaUrgenteModificada);
        }

        public async Task QuitarPrioridadTarea(int id, TareaDTO dto)
        {
            if (dto == null)
            {
                throw new Exception("Los datos para quitar la prioridad han llegado nulos");
            }
            var tareaUrgente = await _repository.ObtenerPorId(id);
            if(tareaUrgente == null)
            {
                throw new Exception("La tarea a la que se está intentando quitar prioridad no existe");
            }
            tareaUrgente.EstaEliminado = true;
            await _repository.Guardar(tareaUrgente);

            var tareaSimple = new Tarea();
            var tareaSimpleModificada = TareaMapper.ModificarEntidadDeTareaUrgente(tareaSimple, dto, tareaUrgente);
            await _repositoryBase.Guardar(tareaSimpleModificada);
        }//Priorizar tarea y quitar prioridad se va a quedar como deuda técnica.

        public async Task CrearTareaUrgente(CrearTareaUrgenteDTO dto, int idUsuario)
        {
            var tareas = await _repository.ObtenerTodos();
            if (dto == null)
            {
                throw new Exception("Los datos para Crear la tarea urgente han llegado nulos");
            }
            var tareaUrgente = TareaUrgenteMapper.CrearEntidad(dto, idUsuario);
            var tareaExistente = tareas.Any(t => t.NombreTarea == dto.NombreTarea && t.IdUsuarioDeLaTarea == idUsuario);
            if (tareaExistente)
            {
                throw new Exception("Esa tarea ya existe");
            }
            await _repository.Guardar(tareaUrgente);
        }
    }
}
