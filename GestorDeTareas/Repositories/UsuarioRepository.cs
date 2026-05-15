using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorDeTareas.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context) { }

        public async Task<Usuario> ObtenerPorCorreo(string correoUsuario)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoUsuario == correoUsuario);
        }

        public async Task<Usuario> ObtenerPorFriendTag(string friendTag)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.FriendTag == friendTag);
        }
    }
}
