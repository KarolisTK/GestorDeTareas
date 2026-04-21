using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Models
{
    public class Usuario
    {
        private int _idUsuario;
        private string _NombreUsuario;
        private string _CorreoUsuario;
        private string _ContraseñaUsuario;
        private bool? _EstaEliminado;


        public int IdUsuario { get { return _idUsuario; } set { _idUsuario = value; } }
        public string NombreUsuario { get { return _NombreUsuario; } set { _NombreUsuario = value; } }
        public string CorreoUsuario { get { return _CorreoUsuario; } set {_CorreoUsuario = value; } }
        public string ContrasenaUsuario { get { return _ContraseñaUsuario; } set { _ContraseñaUsuario = value; } }
        public bool? EstaEliminado { get => _EstaEliminado; set { _EstaEliminado = value; } }

    }
}
