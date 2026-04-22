using GestorDeTareas.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int IdUsuario { get { return _idUsuario; } private set { _idUsuario = value; } }
        public string NombreUsuario { get { return _NombreUsuario; } private set { _NombreUsuario = value; } }
        public string CorreoUsuario { get { return _CorreoUsuario; } private set {_CorreoUsuario = value; } }
        public string ContrasenaUsuario { get { return _ContrasenaUsuario; } private set { _ContrasenaUsuario = value; } }
        public bool? EstaEliminado { get => _EstaEliminado; private set { _EstaEliminado = value; } }

        public Usuario CrearUsuarioConDTO(UsuarioDTO dto)
        {
            return new Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                CorreoUsuario = dto.CorreoUsuario,
                ContrasenaUsuario = dto.ContrasenaUsuario,
                EstaEliminado = false,

            };
        }

        public void EditarUsuarioConDTO(EditarUsuarioDTO dto)
        {
            if (dto.NombreUsuario != null)
                NombreUsuario = dto.NombreUsuario;

            if (dto.CorreoUsuario != null)
                CorreoUsuario = dto.CorreoUsuario;

            if (dto.ContrasenaUsuario != null)
                ContrasenaUsuario = dto.ContrasenaUsuario;

            if (dto.EstaEliminado != null)
                EstaEliminado = dto.EstaEliminado;
        }

        public void MarcarUsuarioComoEliminado()
        {
            EstaEliminado = true;
        }
    }
}
