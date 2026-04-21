using GestorDeTareas.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GestorDeTareas
{
    public class TareasPorUsuariosRepository
    {
        private readonly string _Ruta;

        public TareasPorUsuariosRepository(string ruta = "TareasPorUsuario.json")
        {
            _Ruta = ruta;
        }

        public List<TareasPorUsuario> CargarListaEnJson()
        {
            if (!File.Exists(_Ruta)) return new List<TareasPorUsuario>();
            return JsonSerializer.Deserialize<List<TareasPorUsuario>>(File.ReadAllText(_Ruta));
        }

        public void GuardarListaEnJson(List<TareasPorUsuario> lista)
        {
            File.WriteAllText(_Ruta, JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
