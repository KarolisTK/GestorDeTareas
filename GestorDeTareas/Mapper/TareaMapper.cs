using AutoMapper;
using GestorDeTareas.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Mapper
{
    public static class TareaMapper
    {
        public static Tarea CrearEntidad(CrearTareaDTO dto, string id)
        {
            return new Tarea
            {
                IdTarea = id,
                NombreTarea = dto.NombreTarea,
                DescripcionTarea = dto.DescripcionTarea,
                FechaCreacionTarea = dto.FechaCreacionTarea,
                EstadoTarea = dto.EstadoTarea,
                EstaEliminado = dto.EstaEliminado,
                TipoTarea = dto.TipoTarea,

            };
        }
        public static void ModificarEntidad(Tarea tarea, EditarTareaDTO dto)
        {
            if (dto.NombreTarea != null)
                tarea.NombreTarea = dto.NombreTarea;

            if (dto.DescripcionTarea != null)
                tarea.DescripcionTarea = dto.DescripcionTarea;

            if (dto.EstadoTarea != null)
                tarea.EstadoTarea = dto.EstadoTarea;

            if (dto.TipoTarea != null)
                tarea.TipoTarea = dto.TipoTarea;

            if (dto.EstaEliminado.HasValue)
                tarea.EstaEliminado = dto.EstaEliminado.Value;
        }
    }
}
