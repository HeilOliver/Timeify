using FluentValidation;
using Timeify.Api.Shared.Models.Request;

namespace Timeify.Api.Models.Validation
{
    public class ExchangeRefreshTokenRequestValidator : AbstractValidator<ExchangeRefreshTokenRequest>
    {
        public ExchangeRefreshTokenRequestValidator()
        {
            RuleFor(x => x.AccessToken)
                .NotEmpty();

            RuleFor(x => x.RefreshToken)
                .NotEmpty();
        }
    }
}