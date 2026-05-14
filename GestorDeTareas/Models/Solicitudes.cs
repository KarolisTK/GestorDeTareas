using GestorDeTareas.Enums;
using GestorDeTareas.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorDeTareas.Models
{
    public class Solicitudes : IEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSolicitud { get; set; }
        public int Id => IdSolicitud;
        public int IdEmisor { get; set; }
        public Usuario Emisor { get; set; }
        public int IdReceptor { get; set; }
        public Usuario Receptor { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public TipoEstadoSolicitud TiposEstado { get; set; }
        public TiposSolicitudes TiposSolicitudes { get; set; }

        public Solicitudes() { }

        public Solicitudes(int idEmisor, int idReceptor, TiposSolicitudes tiposSolicitudes)
        {
            IdEmisor = idEmisor;
            IdReceptor = idReceptor;
            FechaSolicitud = DateTime.UtcNow;
            TiposEstado = TipoEstadoSolicitud.Pendiente;
            TiposSolicitudes = tiposSolicitudes;
        }
    }
}
