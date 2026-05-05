using System.Net;

namespace InnoClinic.Shared.Exceptions
{
    public class NotFoundException : BasicException
    {
        public NotFoundException(string entityName)
            : base(string.Format(SharedMessages.NotFoundMessage, entityName), HttpStatusCode.NotFound)
        {
        }
    }
}
