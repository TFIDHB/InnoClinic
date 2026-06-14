using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class CreateSpecializationRequestDtoValidator : AbstractValidator<CreateSpecializationRequestDto>
    {
        public CreateSpecializationRequestDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.ServiceIds)
                .NotNull()
                .Must(ids => ids != null && ids.Any()).WithMessage(ServiceMessages.MinimumOneRowMessage);

            RuleForEach(x => x.ServiceIds)
                .NotEmpty();
        }
    }
}
