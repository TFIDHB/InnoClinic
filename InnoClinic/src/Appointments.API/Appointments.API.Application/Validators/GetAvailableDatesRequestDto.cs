using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class GetAvailableDatesRequestValidator : AbstractValidator<GetAvailableDatesRequestDto>
    {
        public GetAvailableDatesRequestValidator()
        {
            RuleFor(x => x.ServiceId)
                .NotEmpty();

            RuleFor(x => x.SpecializationId)
                .NotEmpty();

            RuleFor(x => x.ServiceType)
                .IsInEnum().WithMessage("Invalid service type");
        }
    }
}
