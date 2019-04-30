using System.Collections.Generic;
using Timeify.Api.Shared.Models.Response;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseResponse
{
    public class GetJobTaskForJobResponse : UseCaseResponseMessage
    {
        public GetJobTaskForJobResponse(IEnumerable<JobTask> tasks, bool success = true, string message = null) : base(
            success, message)
        {
            Tasks = tasks;
        }

        public GetJobTaskForJobResponse(IEnumerable<ApplicationError> errors, bool success = false,
            string message = null) : base(
            success, message)
        {
            Errors = errors;
        }

        public IEnumerable<ApplicationError> Errors { get; }

        public IEnumerable<JobTask> Tasks { get; }
    }
}