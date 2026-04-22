using GestorDeTareas;
using GestorDeTareas.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Tarea
{
    private int _IdTarea;
    private string _NombreTarea;
    private string _DescripcionTarea;
    private DateTime _FechaCreacionTarea;
    private EstadosTarea? _EstadosTarea;
    private bool? _EstaEliminado;
    private TiposTarea? _TiposTarea;
    private int _IdUsuarioDeLaTarea;


    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdTarea { get { return _IdTarea; } private set { _IdTarea = value; } }
    public string NombreTarea { get { return _NombreTarea; } private set { _NombreTarea = value; } }
    public string DescripcionTarea { get { return _DescripcionTarea; } private set { _DescripcionTarea = value; } }
    public DateTime FechaCreacionTarea { get => _FechaCreacionTarea; private set { _FechaCreacionTarea = value; } }
    public EstadosTarea? EstadosTarea { get => _EstadosTarea; private set { _EstadosTarea = value; } }
    public bool? EstaEliminado {  get => _EstaEliminado; private set { _EstaEliminado = value; } }
    public TiposTarea? TiposTarea { get => _TiposTarea; private set { _TiposTarea = value; } }
    public int IdUsuarioDeLaTarea { get => _IdUsuarioDeLaTarea; private set { _IdUsuarioDeLaTarea = value; } }

    public Tarea CrearTareaConDto(CrearTareaDTO dto)
    {
        return new Tarea
        {
            NombreTarea = dto.NombreTarea,
            DescripcionTarea = dto.DescripcionTarea,
            FechaCreacionTarea = dto.FechaCreacionTarea,
            EstadosTarea = dto.EstadosTarea,
            EstaEliminado = dto.EstaEliminado,
            TiposTarea = dto.TiposTarea,
            IdUsuarioDeLaTarea = Sesion.IdUsuarioSesionActiva

        };
    }
    public void EditarTareaConDTO(EditarTareaDTO dto)
    {
        if (dto.NombreTarea != null)
            NombreTarea = dto.NombreTarea;

        if (dto.DescripcionTarea != null)
            DescripcionTarea = dto.DescripcionTarea;

        if (dto.EstadosTarea != null)
            EstadosTarea = dto.EstadosTarea;

        if (dto.TiposTarea != null)
            TiposTarea = dto.TiposTarea;

        if (dto.EstaEliminado.HasValue)
            EstaEliminado = dto.EstaEliminado.Value;
    }

    public void MarcarTareaComoEliminada(Tarea tareaFiltrada)
    {
        tareaFiltrada.EstaEliminado = true;
    }
}


