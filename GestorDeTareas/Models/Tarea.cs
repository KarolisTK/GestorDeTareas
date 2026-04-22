using GestorDeTareas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
    public int IdTarea { get { return _IdTarea; } set { _IdTarea = value; } }
    public string NombreTarea { get { return _NombreTarea; } set { _NombreTarea = value; } }
    public string DescripcionTarea { get { return _DescripcionTarea; } set { _DescripcionTarea = value; } }
    public DateTime FechaCreacionTarea { get => _FechaCreacionTarea; set { _FechaCreacionTarea = value; } }
    public EstadosTarea? EstadosTarea { get => _EstadosTarea; set { _EstadosTarea = value; } }
    public bool? EstaEliminado {  get => _EstaEliminado; set { _EstaEliminado = value; } }
    public TiposTarea? TiposTarea { get => _TiposTarea; set { _TiposTarea = value; } }
    public int IdUsuarioDeLaTarea { get => _IdUsuarioDeLaTarea; set { _IdUsuarioDeLaTarea = value; } }
}
