using Timeify.Api.Shared.Auth;

namespace Timeify.Api.Shared.Models.Response
{
    public class ExchangeRefreshTokenResponse
    {
        public ExchangeRefreshTokenResponse()
        {
        }

        public ExchangeRefreshTokenResponse(AccessToken accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public AccessToken AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}