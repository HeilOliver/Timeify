using FluentValidation;
using Timeify.Api.Shared.Models.Request;

namespace Timeify.Api.Models.Validation
{
    public class DeleteJobTaskRequestValidator : AbstractValidator<DeleteJobTaskRequest>
    {
        public DeleteJobTaskRequestValidator()
        {
            RuleFor(x => x.TaskId)
                .GreaterThan(0);
        }
    }
}