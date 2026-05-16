using GestorDeTareas.Enums;

namespace GestorDeTareas.DTOs
{
    public class SolicitudesDTO
    {
        public int IdSolicitud { get; set; }
        public int IdSolicitante { get; set; }
        public int IdSolicitado { get; set; }
        public string NombreSolicitante { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public TipoEstadoSolicitud Estado { get; set; }
        public TiposSolicitudes TiposSolicitudes { get; set; }
    }
}
