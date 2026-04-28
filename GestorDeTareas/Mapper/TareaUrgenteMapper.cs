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
            (
                dto.NombreTarea,
                dto.DescripcionTarea,
                DateTime.Now,
                dto.EstadosTarea,
                dto.EstaEliminado = false,
                TiposTarea.Urgente,
                Sesion.IdUsuarioSesionActiva,
                dto.TienePrioridad,
                dto.FechaLimite
            );
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
