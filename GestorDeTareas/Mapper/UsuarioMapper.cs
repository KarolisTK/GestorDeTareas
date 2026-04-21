using GestorDeTareas.DTOs;
using GestorDeTareas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorDeTareas.Mapper
{
    public class UsuarioMapper
    {
        public static Usuario CrearUsuario(string id, UsuarioDTO dto)
        {
            return new Usuario
            {
                IdUsuario = id,
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Tareas = dto.Tareas,
                EstaEliminado = false,
                
            };
        }
        public static void ModificarUsuario(Usuario usuario, EditarUsuarioDTO dto)
        {
            if (dto.Name != null)
                usuario.Name = dto.Name;

            if (dto.Email != null)
                usuario.Email = dto.Email;

            if (dto.Password != null)
                usuario.Password = dto.Password;

            if (dto.EstaEliminado != null)
                usuario.EstaEliminado = dto.EstaEliminado;
        }
    }
}
