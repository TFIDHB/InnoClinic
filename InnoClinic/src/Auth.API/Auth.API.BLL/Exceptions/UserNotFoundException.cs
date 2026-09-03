using InnoClinic.Shared.Exceptions;

namespace BLL.Exceptions
{
    public class UserNotFoundException : BadRequestException
    {
        public UserNotFoundException()
            : base(BllMessages.UserNotFoundMessage)
        {
        }
    }
}
