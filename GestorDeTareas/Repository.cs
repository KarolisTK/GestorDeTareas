using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
using Microsoft.EntityFrameworkCore;
namespace GestorDeTareas
{
    public class Repository<T> : IRepositorio<T> where T : class, IEntidad
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public async Task< List<T>> ObtenerTodos()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> ObtenerPorId(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Guardar(T entidad)
        {
            if (entidad.Id != 0)
                _context.Set<T>().Update(entidad);
            else
                _context.Set<T>().Add(entidad);

           await _context.SaveChangesAsync();

        }
    }
}
