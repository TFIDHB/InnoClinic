using InnoClinic.Shared.Exceptions;
using System.Net;

namespace Application.Exceptions
{
    internal class OverlappingAppointmentException : BadRequestException
    {
        public OverlappingAppointmentException() : base(Messages.OverlappingAppointmentException) { }
    }
}
