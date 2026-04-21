using GestorDeTareas.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GestorDeTareas
{
    public class UsuarioRepository
    {
        private readonly string _Ruta;

        public UsuarioRepository(string ruta = "Usuarios.json")
        {
            _Ruta = ruta;
        }
        public List<Usuario> CargarListaEnJson()
        {
            if (!File.Exists(_Ruta)) return new List<Usuario>();
            return JsonSerializer.Deserialize<List<Usuario>>(File.ReadAllText(_Ruta));
        }

        public Usuario CargarSoloUnUsuarioPorID(int id)
        {
            var Usuario = CargarListaEnJson();
            var UsuarioFiltrado = Usuario.Where(u => u.IdUsuario == id).FirstOrDefault();
            return UsuarioFiltrado;
        }

        public void GuardarListaEnJson(List<Usuario> lista)
        {
            File.WriteAllText(_Ruta, JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
