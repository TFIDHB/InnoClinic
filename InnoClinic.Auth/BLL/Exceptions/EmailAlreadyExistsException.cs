using InnoClinic.Shared.Exceptions;
using System.Net;

namespace BLL.Exceptions
{
    public class EmailAlreadyExistsException : BasicException
    {
        public EmailAlreadyExistsException() : base(BllMessages.EmailExists, HttpStatusCode.BadRequest) { }
    }
}
