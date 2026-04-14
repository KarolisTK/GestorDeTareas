using GestorDeTareas.DTOs;
using GestorDeTareas.Mapper;

namespace GestorDeTareas
{
    public class TareaService
    {
        private TareaRepository _repository;

        TareaRepository repository = new TareaRepository();
        public Tarea MapearTarea(TareaDTO dto)
        {
            var id = Guid.NewGuid();
            var parseID = id.ToString();
            return TareaMapper.ToModel(dto, parseID);
        }

        public void CrearTarea(TareaDTO dto)
        {
            var lista = repository.CargarListaEnJson();
            var tarea = MapearTarea(dto);
            lista.Add(tarea);
            repository.GuardarListaEnJson(lista);
        }

        public Tarea AplicarCambiosDeEdiccionAUnaTarea(Tarea tarea, TareaDTO dto)
        {
            tarea.NombreTarea = dto.NombreTarea;
            tarea.DescripcionTarea = dto.DescripcionTarea;
            return tarea;
        }

        public void EditarTarea(string id, TareaDTO dto)
        {
            var lista = repository.CargarListaEnJson();
            var tareaFiltrada = lista.FirstOrDefault(t => t.IdTarea == id);
            AplicarCambiosDeEdiccionAUnaTarea(tareaFiltrada, dto);
            repository.GuardarListaEnJson(lista);
        }

        public Tarea MarcarTareaComoEliminada(Tarea tarea, TareaDTO dto)
        {
            tarea.EstaEliminado = dto.EstaEliminado;
            return tarea;
        }
        public void EliminarTarea(string id, TareaDTO dto)
        {
            var lista = repository.CargarListaEnJson();
            var tareaFiltrada = lista.FirstOrDefault(t => t.IdTarea == id);
            var marcadaComoEliminada = MarcarTareaComoEliminada(tareaFiltrada, dto);
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

        public void SacarSoloUnaTareaPorPantalla(string id)
        {
            var tareaPorId = repository.CargarSoloUnaTareaPorID(id);
            Console.WriteLine(tareaPorId.NombreTarea + " " + tareaPorId.DescripcionTarea + " " + tareaPorId.EstaEliminado);
        }
    }
}
