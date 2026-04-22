using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GestorDeTareas.Models
{
    public class Usuario
    {
        
        private int _idUsuario;
        private string _NombreUsuario;
        private string _CorreoUsuario;
        private string _ContrasenaUsuario;
        private bool? _EstaEliminado;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get { return _idUsuario; } set { _idUsuario = value; } }
        public string NombreUsuario { get { return _NombreUsuario; } set { _NombreUsuario = value; } }
        public string CorreoUsuario { get { return _CorreoUsuario; } set {_CorreoUsuario = value; } }
        public string ContrasenaUsuario { get { return _ContrasenaUsuario; } set { _ContrasenaUsuario = value; } }
        public bool? EstaEliminado { get => _EstaEliminado; set { _EstaEliminado = value; } }

    }
}
