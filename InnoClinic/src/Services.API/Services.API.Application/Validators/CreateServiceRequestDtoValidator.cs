using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class CreateServiceRequestDtoValidator : AbstractValidator<CreateServiceRequestDto>
    {
        public CreateServiceRequestDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.ServiceCategoryId)
                .NotEmpty();

            RuleFor(x => x.SpecializationId)
                .NotEmpty();
        }
    }
}
