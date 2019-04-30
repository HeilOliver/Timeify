using System.Collections.Generic;
using Timeify.Api.Shared.Models.Response;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseResponse
{
    public class GetUserResponse : UseCaseResponseMessage
    {
        public GetUserResponse(CompleteUser user, bool success = true, string message = null) : base(
            success, message)
        {
            User = user;
        }

        public GetUserResponse(IEnumerable<ApplicationError> errors, bool success = false, string message = null) :
            base(
                success, message)
        {
            Errors = errors;
        }

        public IEnumerable<ApplicationError> Errors { get; }

        public CompleteUser User { get; }
    }
}