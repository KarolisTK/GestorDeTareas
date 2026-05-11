using GestorDeTareas.Enums;
using GestorDeTareas.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorDeTareas.Models
{
    public class Notificaciones : IEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNotificacion {  get; set; }
        public int Id => IdNotificacion;
        public int IdEmisor {  get; set; }
        public Usuario Emisor { get; set; }
        public int IdReceptor { get; set; }
        public Usuario Receptor { get; set; }
        public DateTime FechaCreacionNotificacion { get; set; }
        public TiposNotificaciones TipoNotificacion {  get; set; }
        public string TituloNotificacion { get; set; }
        public string ContenidoNotificacion { get; set; }
        public bool MarcadoComoLeido {  get; set; }


        public Notificaciones() { }
    }
}
