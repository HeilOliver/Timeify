using FluentValidation;
using Timeify.Api.Shared.Models.Request;

namespace Timeify.Api.Models.Validation
{
    public class DeleteJobRequestValidator : AbstractValidator<DeleteJobRequest>
    {
        public DeleteJobRequestValidator()
        {
            RuleFor(x => x.JobId)
                .GreaterThan(0);
        }
    }
}