using AutoMapper;
using GestorDeTareas.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Mapper
{
    public static class TareaMapper
    {
        public static Tarea ToModel(TareaDTO dto, string id)
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
        public static TareaDTO ToDto(Tarea tarea)
        {
            return new TareaDTO
            {
                NombreTarea = tarea.NombreTarea,
                DescripcionTarea = tarea.DescripcionTarea,
                FechaCreacionTarea = tarea.FechaCreacionTarea,
                EstadoTarea = tarea.EstadoTarea,
                EstaEliminado = tarea.EstaEliminado,
                TipoTarea = tarea.TipoTarea,
            };
        }
    }
}
