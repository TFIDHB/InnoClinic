using InnoClinic.Shared.Exceptions;
using System.Net;

namespace Application.Exceptions
{
    public class InactiveEntityException : BadRequestException
    {
        public InactiveEntityException(string entityName) : base(string.Format(Messages.InactiveEntityMessage, entityName)) { }
    }
}