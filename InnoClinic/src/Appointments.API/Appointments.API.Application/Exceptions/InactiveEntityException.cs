using InnoClinic.Shared.Exceptions;
using System.Net;

namespace Application.Exceptions
{
    public class InactiveEntityException : BasicException
    {
        public InactiveEntityException(string entityName) : base(string.Format(Messages.InactiveEntityMessage, entityName), HttpStatusCode.BadRequest) { }
    }
}