namespace BLL.Exceptions
{
    public class ExpeditionNotAllowedException : Exception
    {
        public ExpeditionNotAllowedException() { }
        public ExpeditionNotAllowedException(string message) : base(message) { }
        public ExpeditionNotAllowedException(string message, Exception inner) : base(message, inner) { }
    }
}
