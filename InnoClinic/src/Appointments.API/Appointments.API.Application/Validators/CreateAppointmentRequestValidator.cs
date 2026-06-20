using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class CreateAppointmentRequestValidator : AbstractValidator<CreateAppointmentRequestDto>
    {
        public CreateAppointmentRequestValidator() {
            RuleFor(x => x.PatientId)
                .NotEmpty();
            RuleFor(x => x.SpecializationId)
                .NotEmpty();
            RuleFor(x => x.DoctorId)
                .NotEmpty();
            RuleFor(x => x.ServiceId)
                .NotEmpty();
            RuleFor(x => x.OfficeId)
                .NotEmpty();
            RuleFor(x => x.Date)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage(Messages.AppointmentInPastMessage);
            RuleFor(x => x.Time)
                .NotEmpty()
                .Must(time => time.Hour is >= 8 and <= 19)
                .WithMessage(Messages.AppointmentBetweenMessage);
        }
    }
}
