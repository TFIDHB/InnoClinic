using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateResultRequestDtoValidator : AbstractValidator<UpdateResultRequestDto>
    {
        public UpdateResultRequestDtoValidator() 
        {
            RuleFor(x => x.Complaints).NotEmpty().WithMessage(AppointmentsApiMessages.ComplainsRequiredMessage);
            RuleFor(x => x.Conclusion).NotEmpty().WithMessage(AppointmentsApiMessages.ConclusionIsRequiredMessage);
            RuleFor(x => x.Recommendations).NotEmpty().WithMessage(AppointmentsApiMessages.RecommendationsAreRequiredMessage);
        }
    }
}
