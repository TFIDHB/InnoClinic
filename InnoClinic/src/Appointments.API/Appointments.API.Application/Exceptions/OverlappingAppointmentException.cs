using InnoClinic.Shared.Exceptions;
using System.Net;

namespace Application.Exceptions
{
    internal class OverlappingAppointmentException : BasicException
    {
        public OverlappingAppointmentException() : base(Messages.OverlappingAppointmentException, HttpStatusCode.BadRequest) { }
    }
}
