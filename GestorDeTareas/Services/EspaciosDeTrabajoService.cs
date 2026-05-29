using GestorDeTareas.DTOs;
using GestorDeTareas.Exceptions;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using GestorDeTareas.Repositories;

namespace GestorDeTareas.Services
{
    public class EspaciosDeTrabajoService : IEspaciosDeTrabajoService
    {
        private readonly IEspaciosDeTrabajoRepository _espaciosDeTrabajorepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public EspaciosDeTrabajoService(IEspaciosDeTrabajoRepository espaciosDeTrabajorepository, IUsuarioRepository usuarioRepository)
        {
            _espaciosDeTrabajorepository = espaciosDeTrabajorepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task CrearEspacioDeTrabajo(int idUsuario, CrearNuevoEspacioDeTrabajoDTO dto)
        {
            var usuario = await _usuarioRepository.ObtenerPorId(idUsuario);
            if (usuario == null)
                throw new NotFoundException("El usuario no existe.");

            var nuevoEspacioDeTrabajo = new EspaciosDeTrabajo
            {
                Nombre = dto.Nombre,
                Usuarios = new List<Usuario> { usuario }
            };
            await _espaciosDeTrabajorepository.Guardar(nuevoEspacioDeTrabajo);
        }

        public async Task AniadirNuevoUsuarioAlEspacioDeTrabajo(AniadirNuevoUsuarioAlEspacioDeTrabajoDTO dto)
        {
            var espacioDeTrabajo = await _espaciosDeTrabajorepository.ObtenerPorId(dto.IdEspacioDeTrabajo);
            if (espacioDeTrabajo == null)
                throw new NotFoundException("El espacio de trabajo no existe.");

            var usuarioAAñadir = await _usuarioRepository.ObtenerPorId(dto.IdUsuario);
            if (usuarioAAñadir == null)
                throw new NotFoundException("El usuario no existe.");

            var yaPertenece = espacioDeTrabajo.Usuarios.Any(u => u.IdUsuario == dto.IdUsuario);
            if (yaPertenece)
                throw new ConflictException("Este usuario ya pertenece a este espacio de trabajo.");

            espacioDeTrabajo.Usuarios.Add(usuarioAAñadir);
            await _espaciosDeTrabajorepository.Guardar(espacioDeTrabajo);
        }

        public async Task<List<MostrarEspaciosDeTrabajoDTO>> MostrarEspaciosDeTrabajoPorUsuario( int idUsuario)
        {
            return await _espaciosDeTrabajorepository.MostrarEspaciosDeTrabajo(idUsuario);
        }

        public async Task EliminarEspacioDeTrabajoPorID(int idEspacioTrabajo)
        {
            var espacioTrabajo = await _espaciosDeTrabajorepository.ObtenerPorId(idEspacioTrabajo);
            if(espacioTrabajo == null)
            {
                throw new NotFoundException("El espacio de trabajo a eliminr no se ha encontrado");
            }
            if(espacioTrabajo.EstaEliminado == true)
            {
                throw new ConflictException("El espacio de trabajo seleccionado ya está elimiando");
            }
            espacioTrabajo.EstaEliminado = true;
            await _espaciosDeTrabajorepository.Guardar(espacioTrabajo);
        }
    }
}
