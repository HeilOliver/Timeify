using System.Collections.Generic;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseResponse
{
    public class CreateJobTaskResponse : UseCaseResponseMessage
    {
        public CreateJobTaskResponse(int taskId, bool success = true, string message = null) : base(
            success, message)
        {
            TaskId = taskId;
        }

        public CreateJobTaskResponse(IEnumerable<ApplicationError> errors, bool success = false,
            string message = null) : base(
            success, message)
        {
            Errors = errors;
            TaskId = -1;
        }

        public int TaskId { get; }

        public IEnumerable<ApplicationError> Errors { get; }
    }
}