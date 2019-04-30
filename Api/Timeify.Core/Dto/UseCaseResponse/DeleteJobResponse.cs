using System.Collections.Generic;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseResponse
{
    public class DeleteJobResponse : UseCaseResponseMessage
    {
        public DeleteJobResponse(bool success = true, string message = null) : base(
            success, message)
        {
        }

        public DeleteJobResponse(IEnumerable<ApplicationError> errors, bool success = false,
            string message = null) : base(
            success, message)
        {
            Errors = errors;
        }

        public IEnumerable<ApplicationError> Errors { get; }
    }
}