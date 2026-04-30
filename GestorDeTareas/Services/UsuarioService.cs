using GestorDeTareas.DTOs;
using GestorDeTareas.Interfaces;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;
namespace GestorDeTareas.Services
{
    public class UsuarioService
    {
        private readonly IRepositorio<Usuario> _repository;

        public UsuarioService(IRepositorio<Usuario> repository)
        {
            _repository = repository;
        }
        public async Task CrearUsuario(UsuarioDTO dto)
        {
            var usuario = UsuarioMapper.CrearUsuario(dto);
            await _repository.Guardar(usuario);
        }
        public async Task EditarUsuario(EditarUsuarioDTO dto, int IdUsuario)
        {
            var usuarioFiltrado = await _repository.ObtenerPorId(IdUsuario);
            if (usuarioFiltrado == null) 
            {
                throw new Exception("El usuario filtrado para editar usuario no existe");
            }
            UsuarioMapper.ModificarUsuario(usuarioFiltrado, dto);
            await _repository.Guardar(usuarioFiltrado);
        }
        public async Task EliminarUsuario(int idUsuario)
        {
            var usuarioFiltrado = await _repository.ObtenerPorId(idUsuario);
            if (usuarioFiltrado == null)
            {
                throw new Exception("El usuario filtrado para eliminar usuario no existe");
            }
            usuarioFiltrado.EstaEliminado = true;
            await _repository.Guardar(usuarioFiltrado);
        }
    }
}
