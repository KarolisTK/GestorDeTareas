namespace GestorDeTareas.Interfaces
{
    public interface ITareasRepository: IRepositorio<Tarea> 
    {
        Task<List<Tarea>> ObtenerPorEspacioYUsuario(int idEspacioDeTrabajo, int idUsuario);
    }
}
