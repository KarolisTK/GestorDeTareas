using GestorDeTareas.Enums;
using GestorDeTareas.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorDeTareas.Models
{
    public class Amigos : IEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAmigos { get; set; }
        public int Id => IdAmigos;
        public int IdEmisor { get; set; }
        public Usuario Emisor { get; set; }
        public int IdReceptor { get; set; }
        public Usuario Receptor { get; set; }
        public DateTime FechaInicioAmistad {  get; set; }
        public TiposEstadoAmistad TiposEstado  { get; set;}

      

        public Amigos() { }

        public Amigos(int idEmisor, int idReceptor)
        {
            IdEmisor = idEmisor;
            IdReceptor = idReceptor;
            FechaInicioAmistad = DateTime.UtcNow;
            TiposEstado = TiposEstadoAmistad.Pendiente;
        }

    }
}
