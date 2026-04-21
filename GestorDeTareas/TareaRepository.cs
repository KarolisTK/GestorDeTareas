using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GestorDeTareas
{
    public class TareaRepository
    {
        private readonly string _Ruta;

        public TareaRepository(string ruta = "Tareas.json")
        {
            _Ruta = ruta;
        }
        public List<Tarea> CargarListaDeUsuarios()
        {
            if (!File.Exists(_Ruta)) return new List<Tarea>();
            return JsonSerializer.Deserialize<List<Tarea>>(File.ReadAllText(_Ruta));
        }

        public Tarea CargarSoloUnaTareaPorID(int id)
        {
            var Tareas = CargarListaDeUsuarios();
            var TareaFiltrada = Tareas.Where(u => u.IdTarea == id).FirstOrDefault();
            return TareaFiltrada;
        }

        public void GuardarLista(List<Tarea> lista)
        {
            File.WriteAllText(_Ruta, JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
