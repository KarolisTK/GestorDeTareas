using GestorDeTareas.DTOs;
using GestorDeTareas.Mapper;
using System.Text.Json;
using File = System.IO.File;

namespace GestorDeTareas
{
    public class TareaService
    {
        private string _Ruta = "Tareas.json";

        private List<Tarea> CargarListaEnJson()
        {
            if (!File.Exists(_Ruta)) return new List<Tarea>();
            return JsonSerializer.Deserialize<List<Tarea>>(File.ReadAllText(_Ruta));
        }

        public Tarea CargarSoloUnaTareaPorID(int id)
        {
            var Tareas = CargarListaEnJson();
            var TareaFiltrada = Tareas.Where(u => u.IdTarea == id).FirstOrDefault();
            return TareaFiltrada;
        }
        public void CrearTarea(TareaDTO dto)
        {
            var ID = new Random().Next(1000);
            var tarea = TareaMapper.ToModel(dto, ID);
            var lista = CargarListaEnJson();
            lista.Add(tarea);
            File.WriteAllText(_Ruta, JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void EditarTarea(int id, TareaDTO dto)
        {
            var lista = CargarListaEnJson();
            var tarea = lista.FirstOrDefault(t => t.IdTarea == id);
            tarea.NombreTarea = dto.NombreTarea;
            tarea.DescripcionTarea = dto.DescripcionTarea;
            File.WriteAllText(_Ruta, JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void SacarTareasPorPantalla()
        {
            var ListaDeTareas = CargarListaEnJson();
            foreach (var tarea in ListaDeTareas)
            {
                Console.WriteLine(tarea.NombreTarea + " " + tarea.DescripcionTarea);
            }
        }
    }
}
