namespace BLL.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base(BllMessages.InvalidPassword) { }
    }
}
