using System.Collections.Generic;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseResponse
{
    public class CreateJobResponse : UseCaseResponseMessage
    {
        public CreateJobResponse(int createdJobId, bool success = true, string message = null) : base(
            success, message)
        {
            CreatedJobId = createdJobId;
        }

        public CreateJobResponse(IEnumerable<ApplicationError> errors, bool success = false,
            string message = null) : base(
            success, message)
        {
            Errors = errors;
            CreatedJobId = default;
        }

        public int CreatedJobId { get; }

        public IEnumerable<ApplicationError> Errors { get; }
    }
}