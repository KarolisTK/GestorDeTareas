using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas
{
    public class TareaUrgenteService : TareaService<TareaUrgente>, IPriorizable
    {
        private readonly IRepositorio<Tarea> _repositoryBase;
        public TareaUrgenteService(
            IRepositorio<TareaUrgente> repository,
            IRepositorio<Tarea> repositorioBase)
            : base(repository)
        {
            _repositoryBase = repositorioBase;
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

        public void QuitarPrioridadTarea(int id)
        {
            var tareaUrgente = _repository.ObtenerPorId(id);
            tareaUrgente.EstaEliminado = true;
            _repository.Guardar(tareaUrgente);

            var tareaSimple = new Tarea
            {
                NombreTarea = tareaUrgente.NombreTarea,
                DescripcionTarea = tareaUrgente.DescripcionTarea,
                FechaCreacionTarea = tareaUrgente.FechaCreacionTarea,
                EstadosTarea = tareaUrgente.EstadosTarea,
                EstaEliminado = false,
                TiposTarea = TiposTarea.Simple,
                IdUsuarioDeLaTarea = tareaUrgente.IdUsuarioDeLaTarea
            };
            _repositoryBase.Guardar(tareaSimple);
        }

        public void CrearTareaUrgente(CrearTareaUrgenteDTO dto)
        {
            var idUsuarioSesion = Sesion.IdUsuarioSesionActiva;
            var tareaUrgente = TareaUrgenteMapper.CrearEntidad(dto);
            _repository.Guardar(tareaUrgente);
        }
    }
}
