using Azure.Core;
using BLL.DTOs;
using FluentValidation;

namespace InnoClinic.Auth.API.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestValidator() {
            RuleFor(request => request.Email)
                .NotEmpty().WithMessage(ApiMessages.EmailRequired)
                .EmailAddress().WithMessage(ApiMessages.EmailInvalid);

            RuleFor(request => request.Password)
                .NotEmpty().WithMessage(ApiMessages.PasswordRequired)
                .Length(6, 15);

            RuleFor(request => request.RePassword)
                .NotEmpty().WithMessage(ApiMessages.RePasswordRequired)
                .Length(6,15)
                .Equal(request => request.Password).WithMessage(ApiMessages.PasswordsMismatch);
        }
    }
}
