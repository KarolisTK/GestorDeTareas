namespace GestorDeTareas.Exceptions
{
    public class SolicitudPendienteException: Exception
    {
        public int StatusCode { get; }
        public SolicitudPendienteException(string message, int statusCode = 456) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
