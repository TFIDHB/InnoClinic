using InnoClinic.Shared.Exceptions;
using System.Net;

namespace BLL.Exceptions
{
    public class EmailAlreadyExistsException : BadRequestException
    {
        public EmailAlreadyExistsException() : base(BllMessages.EmailExists) { }
    }
}
