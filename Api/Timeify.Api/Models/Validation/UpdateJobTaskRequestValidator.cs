using System;
using FluentValidation;
using Timeify.Api.Shared.Models.Request;

namespace Timeify.Api.Models.Validation
{
    public class UpdateJobTaskRequestValidator : AbstractValidator<UpdateJobTaskRequest>
    {
        public UpdateJobTaskRequestValidator()
        {
            RuleFor(x => x.TaskId)
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1024);

            RuleFor(x => x.FinishDate)
                .GreaterThanOrEqualTo(p => DateTime.Now);
        }
    }
}