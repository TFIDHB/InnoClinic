using InnoClinic.Shared.Exceptions;

namespace BLL.Exceptions
{
    public class EmailAlreadyExistsException : BadRequestException
    {
        public EmailAlreadyExistsException()
            : base(BllMessages.EmailExistsMessage)
        {
        }
    }
}
