using GestorDeTareas;
using System;
using System.Collections.Generic;
using System.Text;

public class Tarea
{
    private string _IdTarea;
    private string _NombreTarea;
    private string _DescripcionTarea;
    private DateTime _FechaCreacionTarea;
    private EstadoTarea? _EstadoTarea;
    private bool? _EstaEliminado;
    private TipoTarea? _TipoTarea;

    public string IdTarea { get { return _IdTarea; } set { _IdTarea = value; } }
    public string NombreTarea { get { return _NombreTarea; } set { _NombreTarea = value; } }
    public string DescripcionTarea { get { return _DescripcionTarea; } set { _DescripcionTarea = value; } }
    public DateTime FechaCreacionTarea { get => _FechaCreacionTarea; set { _FechaCreacionTarea = value; } }
    public EstadoTarea? EstadoTarea { get => _EstadoTarea; set { _EstadoTarea = value; } }
    public bool? EstaEliminado {  get => _EstaEliminado; set { _EstaEliminado = value; } }
    public TipoTarea? TipoTarea { get => _TipoTarea; set { _TipoTarea = value; } }
}
