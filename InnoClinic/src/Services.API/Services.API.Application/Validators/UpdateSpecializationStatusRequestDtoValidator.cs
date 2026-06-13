using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateSpecializationStatusRequestDtoValidator : AbstractValidator<UpdateSpecializationStatusRequestDto>
    {
        public UpdateSpecializationStatusRequestDtoValidator() 
        {
            RuleFor(x => x.IsActive)
                .NotEmpty();
        }
    }
}
