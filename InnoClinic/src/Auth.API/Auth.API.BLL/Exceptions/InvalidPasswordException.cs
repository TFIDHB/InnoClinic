using InnoClinic.Shared.Exceptions;

namespace BLL.Exceptions
{
    public class InvalidPasswordException : BadRequestException
    {
        public InvalidPasswordException()
            : base(BllMessages.InvalidPasswordMessage)
        {
        }
    }
}
