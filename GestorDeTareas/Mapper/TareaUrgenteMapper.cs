using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Mapper
{
    public static class TareaUrgenteMapper
    {
        public static TareaUrgente CrearEntidad(CrearTareaUrgenteDTO dto)
        {
            return new TareaUrgente
            {
                NombreTarea = dto.NombreTarea,
                DescripcionTarea = dto.DescripcionTarea,
                FechaCreacionTarea = DateTime.Now,
                EstadosTarea = dto.EstadosTarea,
                EstaEliminado = false,
                TiposTarea = TiposTarea.Urgente,
                IdUsuarioDeLaTarea = Sesion.IdUsuarioSesionActiva,
                FechaLimite = dto.FechaLimite,
                TienePrioridad = dto.TienePrioridad
            };
        }

        public static TareaUrgente ModificarEntidad(TareaUrgente tareaUrgente, CrearTareaUrgenteDTO dto, Tarea tarea)
        {
            tareaUrgente.NombreTarea = dto.NombreTarea ?? tarea.NombreTarea;
            tareaUrgente.DescripcionTarea = dto.DescripcionTarea ?? tarea.DescripcionTarea;
            tareaUrgente.FechaCreacionTarea = tarea.FechaCreacionTarea;
            tareaUrgente.EstadosTarea = dto.EstadosTarea ?? tarea.EstadosTarea;
            tareaUrgente.EstaEliminado = false;
            tareaUrgente.TiposTarea = TiposTarea.Urgente;
            tareaUrgente.IdUsuarioDeLaTarea = tarea.IdUsuarioDeLaTarea;
            tareaUrgente.FechaLimite = dto.FechaLimite;
            tareaUrgente.TienePrioridad = dto.TienePrioridad;
            return tareaUrgente;
        }
    }
}
