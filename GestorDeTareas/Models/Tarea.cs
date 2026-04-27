using GestorDeTareas.DTOs;
using GestorDeTareas.Enums;
using GestorDeTareas.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Tarea : IEntidad
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdTarea { get; init; }
    public int Id => IdTarea;
    public string NombreTarea { get; set; }
    public string DescripcionTarea { get; set; }
    public DateTime FechaCreacionTarea { get; set; }
    public EstadosTarea? EstadosTarea { get; set; }
    public bool? EstaEliminado { get; set; }
    public TiposTarea? TiposTarea { get; set; }
    public int IdUsuarioDeLaTarea { get; set; }
}
//TODO: Constructor


