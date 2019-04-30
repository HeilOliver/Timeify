using FluentValidation;
using Timeify.Api.Shared.Models.Request;

namespace Timeify.Api.Models.Validation
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}