using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorDeTareas.Models
{
    public class Usuario : IEntidad
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; init; }
        public int Id => IdUsuario;
        public string NombreUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string ContrasenaUsuario { get; set; }
        public bool? EstaEliminado { get; set; }

        public Usuario( string nombreUsuario, string correoUsuario, string contrasenaUsuario, bool? estaEliminado)
        {
            NombreUsuario = nombreUsuario;
            CorreoUsuario = correoUsuario;
            ContrasenaUsuario = contrasenaUsuario;
            EstaEliminado = estaEliminado;
        }
    }
}
