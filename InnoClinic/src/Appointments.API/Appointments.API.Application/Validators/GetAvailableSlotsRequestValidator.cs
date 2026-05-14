using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class GetAvailableSlotsRequestValidator : AbstractValidator<GetAvailableSlotsRequestDto>
    {
        public GetAvailableSlotsRequestValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Please, select the date")
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

            RuleFor(x => x.ServiceId)
                .NotEmpty();

            RuleFor(x => x.SpecializationId)
                .NotEmpty();

            RuleFor(x => x.ServiceType)
                .IsInEnum().WithMessage("Invalid service type");
        }
    }
}
