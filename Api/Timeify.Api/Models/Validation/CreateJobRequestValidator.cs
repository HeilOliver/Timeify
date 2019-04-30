using FluentValidation;
using Timeify.Api.Shared.Models.Request;

namespace Timeify.Api.Models.Validation
{
    public class CreateJobRequestValidator : AbstractValidator<CreateJobRequest>
    {
        public CreateJobRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1024);
        }
    }
}