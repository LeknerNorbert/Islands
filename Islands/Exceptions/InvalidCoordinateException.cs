namespace Islands.Exceptions
{
    public class InvalidCoordinateException : Exception
    {
        public InvalidCoordinateException() { }
        public InvalidCoordinateException(string message) : base(message) { }
        public InvalidCoordinateException(string message, Exception inner) : base(message, inner) { }
    }
}
