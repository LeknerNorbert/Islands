namespace Islands.Exceptions
{
    public class InsufficientSkillPointsException : Exception
    {
        public InsufficientSkillPointsException() { }
        public InsufficientSkillPointsException(string message) : base(message) { }
        public InsufficientSkillPointsException(string message, Exception inner) : base(message, inner) { }
    }
}
