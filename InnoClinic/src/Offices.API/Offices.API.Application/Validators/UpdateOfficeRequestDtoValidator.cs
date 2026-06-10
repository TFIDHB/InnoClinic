using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateOfficeRequestDtoValidator : AbstractValidator<UpdateOfficeRequestDto>
    {
        public UpdateOfficeRequestDtoValidator()
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
