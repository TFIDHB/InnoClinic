using InnoClinic.Shared.Exceptions;
using System.Net;

namespace BLL.Exceptions
{
    public class InvalidPasswordException : BasicException
    {
        public InvalidPasswordException() : base(BllMessages.InvalidPassword, HttpStatusCode.BadRequest) { }
    }
}
