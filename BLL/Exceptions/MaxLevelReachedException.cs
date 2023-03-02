namespace BLL.Exceptions
{
    public class MaxLevelReachedException : Exception
    {
        public MaxLevelReachedException() { }
        public MaxLevelReachedException(string message) : base(message) { }
        public MaxLevelReachedException(string message, Exception inner) : base(message, inner) { }
    }
}
