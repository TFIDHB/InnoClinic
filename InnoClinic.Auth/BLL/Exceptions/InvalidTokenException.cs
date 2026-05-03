using InnoClinic.Shared.Exceptions;
using System.Net;

namespace BLL.Exceptions
{
    public class InvalidTokenException : BasicException
    {
        public InvalidTokenException() : base(BllMessages.InvalidToken, HttpStatusCode.BadRequest) { }
    }
}
