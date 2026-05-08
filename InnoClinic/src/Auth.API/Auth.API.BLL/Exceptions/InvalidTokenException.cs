using InnoClinic.Shared.Exceptions;
using System.Net;

namespace BLL.Exceptions
{
    public class InvalidTokenException : BadRequestException
    {
        public InvalidTokenException() : base(BllMessages.InvalidToken) { }
    }
}
