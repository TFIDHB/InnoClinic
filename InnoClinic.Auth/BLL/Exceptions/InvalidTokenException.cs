namespace BLL.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException() : base(BllMessages.InvalidToken) { }
    }
}
