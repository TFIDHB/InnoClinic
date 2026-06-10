using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class CreateOfficeRequestDtoValidator : AbstractValidator<CreateOfficeRequestDto>
    {
        public CreateOfficeRequestDtoValidator()
        {
            RuleFor(e => e.Address)
                .NotEmpty();

            RuleFor(e => e.PhotoId)
                .NotEmpty();

            RuleFor(e => e.RegistryPhoneNumber)
                .NotEmpty();
        }
    }
}
