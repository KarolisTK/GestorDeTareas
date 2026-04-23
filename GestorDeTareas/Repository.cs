using GestorDeTareas.Interfaces;
using GestorDeTareas.Models;
namespace GestorDeTareas
{
    public class Repository<T> : IRepositorio<T> where T : class, IEntidad
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public List<T> ObtenerTodos()
        {
            return _context.Set<T>().ToList();
        }

        public T ObtenerPorId(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Guardar(T entidad)
        {
            if (entidad.Id != 0)
                _context.Set<T>().Update(entidad);
            else
                _context.Set<T>().Add(entidad);

            _context.SaveChanges();

        }
    }
}
