using InnoClinic.Shared.Exceptions;
using System.Net;

namespace BLL.Exceptions
{
    public class UserNotFoundException : BadRequestException
    {
        public UserNotFoundException() : base(BllMessages.UserNotFound) { }
    }
}
