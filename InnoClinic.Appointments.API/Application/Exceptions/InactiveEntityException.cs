namespace Application.Exceptions
{
    public class InactiveEntityException : Exception
    {
        public InactiveEntityException(string entityName) : base(string.Format(Messages.InactiveEntityMessage, entityName)) { }
    }
}