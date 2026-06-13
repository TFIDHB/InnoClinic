using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateServiceRequestDtoValidator : AbstractValidator<UpdateServiceRequestDto>
    {
        public UpdateServiceRequestDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage(ServiceMessages.InvalidPriceMessage);

            RuleFor(x => x.ServiceCategoryId)
                .NotEmpty();

            RuleFor(x => x.SpecializationId)
                .NotEmpty();
        }
    }
}
