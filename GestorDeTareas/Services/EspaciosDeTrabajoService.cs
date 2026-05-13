using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using GestorDeTareas.Repositories;

namespace GestorDeTareas.Services
{
    public class EspaciosDeTrabajoService
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
            var NuevoEspacioDeTrabajo = new EspaciosDeTrabajo
            {
                Nombre = dto.Nombre,
                Usuarios = new List<Usuario> { usuario }
            };
            await _espaciosDeTrabajorepository.Guardar(NuevoEspacioDeTrabajo);

        }
    }
}
