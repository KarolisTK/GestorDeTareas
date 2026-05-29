namespace GestorDeTareas.Exceptions
{
    public class PasswordException:Exception
    {
        public int StatusCode { get; }
        public PasswordException(string message, int statusCode = 467) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
