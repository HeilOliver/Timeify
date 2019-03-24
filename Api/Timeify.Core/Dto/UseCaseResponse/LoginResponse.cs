using System.Collections.Generic;
using Timeify.Api.Shared.Auth;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseResponse
{
    public class LoginResponse : UseCaseResponseMessage
    {
        public LoginResponse(IEnumerable<ApplicationError> errors, bool success = false, string message = null) : base(
            success, message)
        {
            Errors = errors;
        }

        public LoginResponse(AccessToken accessToken, string refreshToken, bool success = false, string message = null)
            : base(success, message)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public AccessToken AccessToken { get; }
        public string RefreshToken { get; }
        public IEnumerable<ApplicationError> Errors { get; }
    }
}