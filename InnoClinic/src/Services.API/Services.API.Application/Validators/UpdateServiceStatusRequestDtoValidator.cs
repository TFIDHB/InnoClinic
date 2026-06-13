using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateServiceStatusRequestDtoValidator : AbstractValidator<UpdateServiceStatusRequestDto>
    {
        public UpdateServiceStatusRequestDtoValidator() 
        {
            RuleFor(x => x.IsActive)
                .NotEmpty();
        }
    }
}
