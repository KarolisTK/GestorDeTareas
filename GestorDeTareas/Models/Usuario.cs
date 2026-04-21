using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Models
{
    public class Usuario
    {
        private string _idUsuario;
        private string _NombreUsuario;
        private string _CorreoUsuario;
        private string _ContraseñaUsuario;
        private bool? _EstaEliminado;


        public string IdUsuario { get { return _idUsuario; } set { _idUsuario = value; } }
        public string Name { get { return _NombreUsuario; } set { _NombreUsuario = value; } }
        public string Email { get { return _CorreoUsuario; } set {_CorreoUsuario = value; } }
        public string Password { get { return _ContraseñaUsuario; } set { _ContraseñaUsuario = value; } }
        public bool? EstaEliminado { get => _EstaEliminado; set { _EstaEliminado = value; } }

    }
}
