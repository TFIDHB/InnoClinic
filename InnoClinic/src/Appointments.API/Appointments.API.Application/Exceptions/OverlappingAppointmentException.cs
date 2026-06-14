using InnoClinic.Shared.Exceptions;
using System.Net;

namespace Application.Exceptions
{
    public class OverlappingAppointmentException : BadRequestException
    {
        public OverlappingAppointmentException() : base(Messages.OverlappingAppointmentException) { }
    }
}
