using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.DTOs
{
    public class TareasPorUsuarioDTO
    {
        public string IdTarea {  get; set; }
        public string IdUsuario { get; set; }

        public TareasPorUsuarioDTO(string idtarea, string idusuario)
        {
            IdTarea = idtarea;
            IdUsuario = idusuario;
        }
    }
    
}
