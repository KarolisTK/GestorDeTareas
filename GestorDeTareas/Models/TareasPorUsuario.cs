using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Models
{
    public class TareasPorUsuario
    {
        private string _IdTareaPorUsuario;
        private string _IdTarea;
        private string _IdUsuario;

        public string IdTareaPorUsuario { get { return _IdTareaPorUsuario; } set { _IdTareaPorUsuario = value; } }
        public string IdTarea { get { return _IdTarea; }set { _IdTarea = value; } }
        public string IdUsuario { get { return _IdUsuario; }set { _IdUsuario = value; } }
    }
}
