using Timeify.Api.Shared.Auth;

namespace Timeify.Api.Shared.Models.Response
{
    public class LoginResponse
    {
        public LoginResponse(AccessToken accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public LoginResponse()
        {
        }

        public AccessToken AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}