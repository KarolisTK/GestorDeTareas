namespace GestorDeTareas.Exceptions
{
    public class FriendException: Exception
    {
        public int StatusCode { get; }
        public FriendException(string message, int statusCode = 455) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
