using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
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
        public void PriorizarTarea(int id, CrearTareaUrgenteDTO dto)
        {
            var tareaOriginal = _repositoryBase.ObtenerPorId(id);
            tareaOriginal.EstaEliminado = true;
            _repositoryBase.Guardar(tareaOriginal);

            var tareaUrgente = new TareaUrgente();
            var tareaUrgenteModificada = TareaUrgenteMapper.ModificarEntidad(tareaUrgente, dto, tareaOriginal);
            _repository.Guardar(tareaUrgenteModificada);
        }

        public void QuitarPrioridadTarea(int id, CrearTareaDTO dto)
        {
            var tareaUrgente = _repository.ObtenerPorId(id);
            tareaUrgente.EstaEliminado = true;
            _repository.Guardar(tareaUrgente);

            var tareaSimple = new Tarea();
            var tareaSimpleModificada = TareaMapper.ModificarEntidadDeTareaUrgente(tareaSimple, dto, tareaUrgente);
            _repositoryBase.Guardar(tareaSimpleModificada);
        }//Priorizar tarea y quitar prioridad se va a quedar como deuda técnica.

        public void CrearTareaUrgente(CrearTareaUrgenteDTO dto, int idUsuario)
        {
            var tareaUrgente = TareaUrgenteMapper.CrearEntidad(dto, idUsuario);
            _repository.Guardar(tareaUrgente);
        }
    }
}
