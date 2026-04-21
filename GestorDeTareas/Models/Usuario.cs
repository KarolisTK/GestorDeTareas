using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Models
{
    public class Usuario
    {
        private string _id;
        private string _name;
        private string _email;
        private string _password;
        private List<Tarea> _tareas;
        private bool? _EstaEliminado;


        public string IdUsuario { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Email { get { return _email; } set {_email = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public List<Tarea>? Tareas { get { return this._tareas; } set { _tareas = value; } }
        public bool? EstaEliminado { get => _EstaEliminado; set { _EstaEliminado = value; } }

    }
}
