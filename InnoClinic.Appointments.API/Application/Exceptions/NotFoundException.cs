namespace Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName) : base(string.Format(Messages.NotFoundMessage, entityName)) { }
    }
}
