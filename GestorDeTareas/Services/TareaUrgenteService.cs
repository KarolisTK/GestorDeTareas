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
            IRepositorio<Tarea> repositorioBase)
            : base(repositorioBase)
        {
            _repositoryBase = repositorioBase;
            _repository = repository;
        }
        public async Task PriorizarTarea(int id, CrearTareaUrgenteDTO dto)
        {
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
            var tareaUrgente = TareaUrgenteMapper.CrearEntidad(dto, idUsuario);
            await _repository.Guardar(tareaUrgente);
        }
    }
}
