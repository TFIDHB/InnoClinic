using System.Net;

namespace InnoClinic.Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName)
            : base(string.Format(SharedMessages.NotFoundMessage, entityName))
        {
        }
    }
}
