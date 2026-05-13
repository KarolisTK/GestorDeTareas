namespace GestorDeTareas.DTOs
{
    public class ListarNotificacionesDTO
    {
        public int IdNotificacion {  get; set; }
        public DateTime FechaCreacionNotificacion { get; set; }
        public string TituloNotificacion { get; set; }
        public string ContenidoNotificacion { get; set; }

        public bool MarcadoComoLeido { get; set; }
    }
}
