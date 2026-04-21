using GestorDeTareas.DTOs;
using GestorDeTareas.Mapper;
using GestorDeTareas.Models;

namespace GestorDeTareas
{
    public class TareaService
    {
        TareaRepository repository = new TareaRepository();
        TareasPorUsuariosRepository tareasPorUsuarioRepsoitory = new TareasPorUsuariosRepository();
        TareasPorUsuariosService tareasPorUsuariosService = new TareasPorUsuariosService();
        public Tarea MapearTarea(CrearTareaDTO dto)
        {
            var id = new Random().Next();
            return TareaMapper.CrearEntidad(dto, id);
        }
        public Tarea MapearEdiccionTarea(Tarea tarea, EditarTareaDTO dto)
        {
            TareaMapper.ModificarEntidad(tarea, dto);
            return tarea;
        }
        public void CrearTarea(CrearTareaDTO dto)
        {
            var idUsuarioSesion = Sesion.IdUsuarioSesionActiva;
            var lista = repository.CargarListaDeUsuarios();
            var tarea = MapearTarea(dto);
            lista.Add(tarea);
            repository.GuardarLista(lista);
            var listaDeTareas = tareasPorUsuarioRepsoitory.CargarListaDeUsuarios();
            var dtoTareas = new TareasPorUsuarioDTO(idUsuarioSesion, tarea.IdTarea);
            var NuevaAsignacion = tareasPorUsuariosService.MapearAsignacionTareasPorUsuario(dtoTareas);
            listaDeTareas.Add(NuevaAsignacion);
            tareasPorUsuarioRepsoitory.GuardarLista(listaDeTareas);
        }
        public void EditarTarea(int id, EditarTareaDTO dto)
        {
            var lista = repository.CargarListaDeUsuarios();
            var tareaFiltrada = FiltrarTareasParaTarea(lista, id);
            MapearEdiccionTarea(tareaFiltrada, dto);
            repository.GuardarLista(lista);
        }
        public void EliminarTarea(int id)
        {
            var lista = repository.CargarListaDeUsuarios();
            var tareaFiltrada = FiltrarTareasParaTarea(lista, id);
            tareaFiltrada.EstaEliminado = true;
            repository.GuardarLista(lista);
        }

        public void SacarTareasPorPantalla()
        {
            var ListaDeTareas = repository.CargarListaDeUsuarios();
            foreach (var tarea in ListaDeTareas)
            {
                Console.WriteLine(tarea.NombreTarea + " " + tarea.DescripcionTarea);
            }
        }

        public Tarea FiltrarTareasParaTarea(List<Tarea> lista, int id)
        {
            return lista.FirstOrDefault(t => t.IdTarea == id);
        }

        public void MostrarTarea(int id)
        {
            var tarea = repository.CargarSoloUnaTareaPorID(id);

            Console.WriteLine("================================");
            Console.WriteLine($"  Nombre:      {tarea.NombreTarea}");
            Console.WriteLine($"  Descripción: {tarea.DescripcionTarea}");
            Console.WriteLine($"  Estado:      {tarea.EstadoTarea}");
            Console.WriteLine($"  Tipo de tarea:      {tarea.TipoTarea}");
            Console.WriteLine($"  Eliminada:   {(tarea.EstaEliminado.GetValueOrDefault() ? "Sí" : "No")}");
            Console.WriteLine("================================");
        }


    }
}
