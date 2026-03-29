namespace BLL.Exceptions
{
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException() : base(BllMessages.EmailExists) { }
    }
}
