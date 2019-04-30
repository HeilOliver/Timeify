using FluentValidation;
using Timeify.Api.Shared.Models.Request;

namespace Timeify.Api.Models.Validation
{
    public class UpdateJobRequestValidator : AbstractValidator<UpdateJobRequest>
    {
        public UpdateJobRequestValidator()
        {
            RuleFor(x => x.JobId)
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1024);
        }
    }
}