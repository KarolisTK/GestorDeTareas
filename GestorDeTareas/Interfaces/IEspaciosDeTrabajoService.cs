using GestorDeTareas.DTOs;

namespace GestorDeTareas.Interfaces
{
    public interface IEspaciosDeTrabajoService
    {
        Task CrearEspacioDeTrabajo(int idUsuario, CrearNuevoEspacioDeTrabajoDTO dto);
        Task AniadirNuevoUsuarioAlEspacioDeTrabajo(AniadirNuevoUsuarioAlEspacioDeTrabajoDTO dto);
        Task<List<MostrarEspaciosDeTrabajoDTO>> MostrarEspaciosDeTrabajoPorUsuario(int idUsuario);
    }
}
