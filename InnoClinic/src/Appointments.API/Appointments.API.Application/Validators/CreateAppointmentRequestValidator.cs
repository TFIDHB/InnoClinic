using Application.DTOs;
using Application.Options;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Validators
{
    public class CreateAppointmentRequestValidator : AbstractValidator<CreateAppointmentRequestDto>
    {
        public CreateAppointmentRequestValidator(IOptions<WorkingHoursOptions> workingHoursOpt)
        {
            var workingHours = workingHoursOpt.Value;

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
                .Must(time => time >= workingHours.Start && time < workingHours.End)
                .WithMessage(string.Format(Messages.AppointmentBetweenMessage, workingHours.Start, workingHours.End));
        }
    }
}
