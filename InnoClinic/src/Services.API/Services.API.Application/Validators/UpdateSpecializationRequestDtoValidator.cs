using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateSpecializationRequestDtoValidator : AbstractValidator<UpdateSpecializationRequestDto>
    {
        public UpdateSpecializationRequestDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.ServiceIds)
                .NotNull();

            RuleForEach(x => x.ServiceIds)
                .NotEmpty();
        }
    }
}
