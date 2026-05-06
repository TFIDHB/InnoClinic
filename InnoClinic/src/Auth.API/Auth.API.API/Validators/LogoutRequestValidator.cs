using BLL.DTOs;
using FluentValidation;

namespace InnoClinic.Auth.API.Validators
{
    public class LogoutRequestValidator : AbstractValidator<LogOutRequestDto>
    {
        public LogoutRequestValidator()
        {
            RuleFor(request => request.RefreshToken)
                .NotEmpty();
        }
    }
}
