using InnoClinic.Shared.Exceptions;
using System.Net;

namespace BLL.Exceptions
{
    public class InvalidPasswordException : BadRequestException
    {
        public InvalidPasswordException() : base(BllMessages.InvalidPassword) { }
    }
}
