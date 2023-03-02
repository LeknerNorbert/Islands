namespace BLL.Exceptions
{
    public class InvalidCoordinatesException : Exception
    {
        public InvalidCoordinatesException() { }
        public InvalidCoordinatesException(string message) : base(message) { }
        public InvalidCoordinatesException(string message, Exception inner) : base(message, inner) { }
    }
}
