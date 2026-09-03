using InnoClinic.Shared.Exceptions;

namespace Application.Exceptions
{
    public class OverlappingAppointmentException : BadRequestException
    {
        public OverlappingAppointmentException()
            : base(AppointmentsApiMessages.OverlappingAppointmentMessage)
        {
        }
    }
}
