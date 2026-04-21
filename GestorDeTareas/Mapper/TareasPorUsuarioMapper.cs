using GestorDeTareas.DTOs;
using GestorDeTareas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Mapper
{
    public class TareasPorUsuarioMapper
    {
        public static TareasPorUsuario CrearAsignacionTareasPorUsuario(int id, TareasPorUsuarioDTO dto)
        {
            return new TareasPorUsuario
            {
                IdTareaPorUsuario = id,
                IdUsuario = dto.IdUsuario,
                IdTarea = dto.IdTarea,

            };
        }
    }
}
