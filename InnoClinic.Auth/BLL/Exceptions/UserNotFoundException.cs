using InnoClinic.Shared.Exceptions;
using System.Net;

namespace BLL.Exceptions
{
    public class UserNotFoundException : BasicException
    {
        public UserNotFoundException() : base(BllMessages.UserNotFound, HttpStatusCode.NotFound) { }
    }
}
