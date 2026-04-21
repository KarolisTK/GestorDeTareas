using GestorDeTareas.DTOs;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas
{
    public class TareasPorUsuariosService
    {
        public TareasPorUsuario MapearAsignacionTareasPorUsuario(TareasPorUsuarioDTO dto)
        {
            var id = new Random().Next();
            return TareasPorUsuarioMapper.CrearAsignacionTareasPorUsuario(id,dto);
        }
    }
}
