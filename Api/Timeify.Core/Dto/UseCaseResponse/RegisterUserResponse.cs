using System.Collections.Generic;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseResponse
{
    public class RegisterUserResponse : UseCaseResponseMessage
    {
        public RegisterUserResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(
            success, message)
        {
            Errors = errors;
        }

        public RegisterUserResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }

        public string Id { get; }
        public IEnumerable<string> Errors { get; }
    }
}