using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class CreateResultRequestDtoValidator : AbstractValidator<CreateResultRequestDto>
    {
        public CreateResultRequestDtoValidator()
        {
            RuleFor(x => x.Complaints).NotEmpty().WithMessage(AppointmentsApiMessages.ComplainsRequiredMessage);
            RuleFor(x => x.Conclusion).NotEmpty().WithMessage(AppointmentsApiMessages.ConclusionIsRequiredMessage);
            RuleFor(x => x.Recommendations).NotEmpty().WithMessage(AppointmentsApiMessages.RecommendationsAreRequiredMessage);
        }
    }
}
