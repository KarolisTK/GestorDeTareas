using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GestorDeTareas
{
    public class TareaRepository
    {
        private readonly AppDbContext _context = new AppDbContext();

        public List<Tarea> CargarListaDeUsuarios()
        {
            return _context.Tareas.ToList();
        }

        public Tarea CargarSoloUnaTareaPorID(int id)
        {
            var Tareas = CargarListaDeUsuarios();
            var TareaFiltrada = Tareas.Where(u => u.IdTarea == id).FirstOrDefault();
            return TareaFiltrada;
        }

        public void GuardarTarea(Tarea tarea)
        {
            if(tarea.IdTarea != 0)
            {
                _context.Tareas.Update(tarea);
                _context.SaveChanges();
            }
            else
            {
                _context.Tareas.Add(tarea);
                _context.SaveChanges();
            }
            
        }
    }
}
