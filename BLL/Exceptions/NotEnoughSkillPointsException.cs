namespace BLL.Exceptions
{
    public class NotEnoughSkillPointsException : Exception
    {
        public NotEnoughSkillPointsException() { }
        public NotEnoughSkillPointsException(string message) : base(message) { }
        public NotEnoughSkillPointsException(string message, Exception inner) : base(message, inner) { }
    }
}
