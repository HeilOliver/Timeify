using System.Collections.Generic;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseResponse
{
    public class UpdateJobResponse : UseCaseResponseMessage
    {
        public UpdateJobResponse(bool success = true, string message = null) : base(
            success, message)
        {
        }

        public UpdateJobResponse(IEnumerable<ApplicationError> errors, bool success = false,
            string message = null) : base(
            success, message)
        {
            Errors = errors;
        }

        public IEnumerable<ApplicationError> Errors { get; }
    }
}