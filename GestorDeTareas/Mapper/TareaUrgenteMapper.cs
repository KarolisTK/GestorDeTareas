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
        public static TareaUrgente CrearEntidad(CrearTareaUrgenteDTO dto, int idUsuario)
        {
            return new TareaUrgente
            (
                dto.NombreTarea,
                dto.DescripcionTarea,
                DateTime.UtcNow,
                dto.EstadosTarea,
                dto.EstaEliminado = false,
                TiposTarea.Urgente,
                idUsuario,
                dto.TienePrioridad,
                dto.FechaLimite,
                dto.IdEspacioDeTrabajo
            );
        }

        public static TareaUrgente ModificarEntidad(TareaUrgente tareaUrgente, CrearTareaUrgenteDTO dto, Tarea tarea)
        {
            tareaUrgente.NombreTarea = tarea.NombreTarea;
            tareaUrgente.DescripcionTarea = tarea.DescripcionTarea;
            tareaUrgente.FechaCreacionTarea = tarea.FechaCreacionTarea;
            tareaUrgente.EstadosTarea = tarea.EstadosTarea;
            tareaUrgente.EstaEliminado = false;
            tareaUrgente.TiposTarea = TiposTarea.Urgente;
            tareaUrgente.IdUsuarioDeLaTarea = tarea.IdUsuarioDeLaTarea;
            tareaUrgente.FechaLimite = dto.FechaLimite;
            tareaUrgente.TienePrioridad = true;
            return tareaUrgente;
        }
    }
}
