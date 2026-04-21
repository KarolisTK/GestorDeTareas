using GestorDeTareas.DTOs;
using GestorDeTareas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Mapper
{
    public class UsuarioMapper
    {
        public static Usuario CrearUsuario(int id, UsuarioDTO dto)
        {
            return new Usuario
            {
                IdUsuario = id,
                NombreUsuario = dto.NombreUsuario,
                CorreoUsuario = dto.CorreoUsuario,
                ContrasenaUsuario = dto.ContrasenaUsuario,
                EstaEliminado = false,
                
            };
        }
        public static void ModificarUsuario(Usuario usuario, EditarUsuarioDTO dto)
        {
            if (dto.NombreUsuario != null)
                usuario.NombreUsuario = dto.NombreUsuario;

            if (dto.CorreoUsuario != null)
                usuario.CorreoUsuario = dto.CorreoUsuario;

            if (dto.ContrasenaUsuario != null)
                usuario.ContrasenaUsuario = dto.ContrasenaUsuario;

            if (dto.EstaEliminado != null)
                usuario.EstaEliminado = dto.EstaEliminado;
        }
    }
}
