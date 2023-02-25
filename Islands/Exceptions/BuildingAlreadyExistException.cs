namespace Islands.Exceptions
{
    public class BuildingAlreadyExistException : Exception
    {
        public BuildingAlreadyExistException() { }
        public BuildingAlreadyExistException(string message) : base(message) { }
        public BuildingAlreadyExistException(string message, Exception inner) : base(message, inner) { }
    }
}
