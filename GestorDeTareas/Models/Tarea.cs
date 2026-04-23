using GestorDeTareas;
using GestorDeTareas.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Tarea
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdTarea { get; init; }
    public string NombreTarea { get; set; }
    public string DescripcionTarea { get; set; }
    public DateTime FechaCreacionTarea { get; init; }
    public EstadosTarea? EstadosTarea { get; set; }
    public bool? EstaEliminado { get; set; }
    public TiposTarea? TiposTarea { get; set; }
    public int IdUsuarioDeLaTarea { get; init; }
}


