using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Models
{
    public class TareasPorUsuario
    {
        private int _IdTareaPorUsuario;
        private int _IdTarea;
        private int _IdUsuario;

        public int IdTareaPorUsuario { get { return _IdTareaPorUsuario; } set { _IdTareaPorUsuario = value; } }
        public int IdTarea { get { return _IdTarea; }set { _IdTarea = value; } }
        public int IdUsuario { get { return _IdUsuario; }set { _IdUsuario = value; } }
    }
}
