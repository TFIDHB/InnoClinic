using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class RescheduleAppointmentRequestDtoValidator : AbstractValidator<RescheduleAppointmentRequestDto>
    {
        public RescheduleAppointmentRequestDtoValidator()
        {
            RuleFor(x => x.DoctorId).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Time).NotEmpty();
            RuleFor(x => x)
                .Must(x => x.Date.ToDateTime(x.Time) > DateTime.UtcNow)
                .WithMessage(AppointmentsApiMessages.AppointmentInPastMessage);
        }
    }
}
