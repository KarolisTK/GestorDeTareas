using AutoMapper;
using GestorDeTareas.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Mapper
{
    public static class TareaMapper
    {
        public static Tarea CrearEntidad(CrearTareaDTO dto)
        {
            return new Tarea
            {
                NombreTarea = dto.NombreTarea,
                DescripcionTarea = dto.DescripcionTarea,
                FechaCreacionTarea = dto.FechaCreacionTarea,
                EstadosTarea = dto.EstadosTarea,
                EstaEliminado = dto.EstaEliminado,
                TiposTarea = dto.TiposTarea,
                IdUsuarioDeLaTarea = Sesion.IdUsuarioSesionActiva

            };
        }
        public static void ModificarEntidad(Tarea tarea, EditarTareaDTO dto)
        {
            if (dto.NombreTarea != null)
                tarea.NombreTarea = dto.NombreTarea;

            if (dto.DescripcionTarea != null)
                tarea.DescripcionTarea = dto.DescripcionTarea;

            if (dto.EstadosTarea != null)
                tarea.EstadosTarea = dto.EstadosTarea;

            if (dto.TiposTarea != null)
                tarea.TiposTarea = dto.TiposTarea;

            if (dto.EstaEliminado.HasValue)
                tarea.EstaEliminado = dto.EstaEliminado.Value;

            if (dto.EstaEliminado.HasValue)
                tarea.EstaEliminado = dto.EstaEliminado.Value;
        }
    }
}
