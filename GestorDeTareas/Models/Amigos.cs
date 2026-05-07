using GestorDeTareas.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorDeTareas.Models
{
    public class Amigos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IdAmigos { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public int IdUsuario2 { get; set; }
        public Usuario Usuario2 { get; set; }
        public DateTime FechaInicioAmistad {  get; set; }
        public TiposEstadoAmistad TiposEstado  { get; set;}

      

        public Amigos() { }

        public Amigos(int idUsuario, int idUsuario2)
        {
            IdUsuario = idUsuario;
            IdUsuario2 = idUsuario2;
            FechaInicioAmistad = DateTime.UtcNow;
            TiposEstado = TiposEstadoAmistad.Pendiente;
        }

    }
}
