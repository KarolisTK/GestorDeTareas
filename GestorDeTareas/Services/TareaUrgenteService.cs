using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Exceptions;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
using GestorDeTareas.Repositories;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Services
{
    public class TareaUrgenteService : TareaService, IPriorizable, ITareaUrgenteService
    {
        private readonly ITareasRepository _tareasRepository;
        private readonly IRepositorio<TareaUrgente> _repository;

        public TareaUrgenteService(
            IRepositorio<TareaUrgente> repository,
            ITareasRepository tareasRepository)
            : base(tareasRepository)
        {
            _repository = repository;
            _tareasRepository = tareasRepository;
        }

        public async Task PriorizarTarea(int id, CrearTareaUrgenteDTO dto)
        {
            if (dto == null)
                throw new ForbiddenException("Los datos para priorizar la tarea han llegado nulos");

            var tareaOriginal = await _tareasRepository.ObtenerPorId(id);
            if (tareaOriginal == null)
                throw new NotFoundException("La tarea que se está intentando priorizar no existe");

            tareaOriginal.EstaEliminado = true;
            await _tareasRepository.Guardar(tareaOriginal);

            var tareaUrgente = new TareaUrgente();
            var tareaUrgenteModificada = TareaUrgenteMapper.ModificarEntidad(tareaUrgente, dto, tareaOriginal);
            await _repository.Guardar(tareaUrgenteModificada);
        }

        public async Task QuitarPrioridadTarea(int id, TareaDTO dto)
        {
            if (dto == null)
                throw new ForbiddenException("Los datos para quitar la prioridad han llegado nulos");

            var tareaUrgente = await _repository.ObtenerPorId(id);
            if (tareaUrgente == null)
                throw new NotFoundException("La tarea a la que se está intentando quitar prioridad no existe");

            tareaUrgente.EstaEliminado = true;
            await _repository.Guardar(tareaUrgente);

            var tareaSimple = new Tarea();
            var tareaSimpleModificada = TareaMapper.ModificarEntidadDeTareaUrgente(tareaSimple, dto, tareaUrgente);
            await _tareasRepository.Guardar(tareaSimpleModificada);
        }

        public async Task CrearTareaUrgente(CrearTareaUrgenteDTO dto, int idUsuario)
        {
            if (dto == null)
                throw new ForbiddenException("Los datos para Crear la tarea urgente han llegado nulos");

            var tareas = await _repository.ObtenerTodos();
            var tareaExistente = tareas.Any(t => t.NombreTarea == dto.NombreTarea && t.IdUsuarioDeLaTarea == idUsuario);
            if (tareaExistente)
                throw new ConflictException("Esa tarea ya existe");

            var tareaUrgente = TareaUrgenteMapper.CrearEntidad(dto, idUsuario);
            await _repository.Guardar(tareaUrgente);
        }
    }
}
