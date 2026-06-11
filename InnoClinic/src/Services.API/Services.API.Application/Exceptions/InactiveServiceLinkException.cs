using Domain.Entities;
using InnoClinic.Shared.Exceptions;

namespace Application.Exceptions
{
    public class InactiveServiceLinkException : BadRequestException
    {
        public InactiveServiceLinkException(Guid serviceId) : base($"Cannot link inactive service with ID {serviceId}.") { }
    }
}
