using FluentValidation;
using Timeify.Api.Shared.Models.Request;

namespace Timeify.Api.Models.Validation
{
    public class GetJobTasksRequestValidator : AbstractValidator<GetJobTasksRequest>
    {
        public GetJobTasksRequestValidator()
        {
            RuleFor(x => x.JobId)
                .GreaterThan(0);
        }
    }
}