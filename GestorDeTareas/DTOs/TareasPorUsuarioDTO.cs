using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.DTOs
{
    public class TareasPorUsuarioDTO
    {
        public int IdTarea {  get; set; }
        public int IdUsuario { get; set; }

        public TareasPorUsuarioDTO(int idtarea, int idusuario)
        {
            IdTarea = idtarea;
            IdUsuario = idusuario;
        }
    }
    
}
