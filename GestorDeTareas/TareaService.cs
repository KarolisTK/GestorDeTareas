using GestorDeTareas.DTOs;
using GestorDeTareas.Mapper;

namespace GestorDeTareas
{
    public class TareaService
    {
        TareaRepository repository = new TareaRepository();
        public Tarea MapearTarea(TareaDTO dto)
        {
            var id = Guid.NewGuid().ToString();
            return TareaMapper.ToModel(dto, id);
        }

        public void CrearTarea(TareaDTO dto)
        {
            var lista = repository.CargarListaEnJson();
            var tarea = MapearTarea(dto);
            lista.Add(tarea);
            repository.GuardarListaEnJson(lista);
        }

        public Tarea MapearCambiosEdiccionTarea(Tarea tarea, TareaDTO dto)
        {
            tarea.NombreTarea = dto.NombreTarea;
            tarea.DescripcionTarea = dto.DescripcionTarea;
            return tarea;
        }

        public void EditarTarea(string id, TareaDTO dto)
        {
            var lista = repository.CargarListaEnJson();
            var tareaFiltrada = FiltrarTareasParaTarea(lista, id);
            MapearCambiosEdiccionTarea(tareaFiltrada, dto);
            repository.GuardarListaEnJson(lista);
        }

        public Tarea MapearTareaComoEliminada(Tarea tarea, TareaDTO dto)
        {
            tarea.EstaEliminado = dto.EstaEliminado;
            return tarea;
        }
        public void EliminarTarea(string id, TareaDTO dto)
        {
            var lista = repository.CargarListaEnJson();
            var tareaFiltrada = FiltrarTareasParaTarea(lista, id);
            var marcadaComoEliminada = MapearTareaComoEliminada(tareaFiltrada, dto);
            repository.GuardarListaEnJson(lista);
        }

        public Tarea MapearCambioEstadoTarea(Tarea tarea, TareaDTO dto)
        {
            tarea.EstadoTarea = dto.EstadoTarea;
            return tarea;
        }

        public void CambiarEstadoTarea(string id, TareaDTO dto)
        {
            var lista = repository.CargarListaEnJson();
            var tareaFiltrada = FiltrarTareasParaTarea(lista, id);
            var cambioEstadoTarea = MapearCambioEstadoTarea(tareaFiltrada, dto);
            repository.GuardarListaEnJson(lista);
        }

        public Tarea MapearTipoTarea(Tarea tarea, TareaDTO dto)
        {
            tarea.TipoTarea = dto.TipoTarea;
            return tarea;
        }

        public void CambiarTipoTarea(string id, TareaDTO dto)
        {
            var lista = repository.CargarListaEnJson();
            var tareaFiltrada = FiltrarTareasParaTarea(lista, id);
            var cambioEstadoTarea = MapearTipoTarea(tareaFiltrada, dto);
            repository.GuardarListaEnJson(lista);
        }

        public void SacarTareasPorPantalla()
        {
            var ListaDeTareas = repository.CargarListaEnJson();
            foreach (var tarea in ListaDeTareas)
            {
                Console.WriteLine(tarea.NombreTarea + " " + tarea.DescripcionTarea);
            }
        }

        public void MostrarTarea(string id)
        {
            var tarea = repository.CargarSoloUnaTareaPorID(id);

            Console.WriteLine("================================");
            Console.WriteLine($"  Nombre:      {tarea.NombreTarea}");
            Console.WriteLine($"  Descripción: {tarea.DescripcionTarea}");
            Console.WriteLine($"  Estado:      {tarea.EstadoTarea}");
            Console.WriteLine($"  Tipo de tarea:      {tarea.TipoTarea}");
            Console.WriteLine($"  Eliminada:   {(tarea.EstaEliminado ? "Sí" : "No")}");
            Console.WriteLine("================================");
        }

        public Tarea FiltrarTareasParaTarea(List<Tarea> lista, string id)
        {
            return lista.FirstOrDefault(t => t.IdTarea == id);
        }
    }
}
