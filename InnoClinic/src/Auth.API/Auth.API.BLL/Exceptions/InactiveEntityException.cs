using InnoClinic.Shared.Exceptions;

namespace BLL.Exceptions
{
    public class InactiveEntityException : BadRequestException
    {
        public InactiveEntityException() : base(BllMessages.InactiveEntityMessage) { }
    }
}
