using InnoClinic.Shared.Exceptions;

namespace BLL.Exceptions
{
    public class InvalidTokenException : BadRequestException
    {
        public InvalidTokenException() : base(BllMessages.InvalidToken) { }
    }
}
