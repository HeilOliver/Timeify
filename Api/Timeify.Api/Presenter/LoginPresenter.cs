using System.Net;
using Timeify.Api.Serialization;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Api.Presenter
{
    [Injectable]
    public sealed class LoginPresenter : IOutputPort<LoginResponse>
    {
        public LoginPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public JsonContentResult ContentResult { get; }

        public void Handle(LoginResponse response)
        {
            ContentResult.StatusCode = (int) (response.Success ? HttpStatusCode.OK : HttpStatusCode.Unauthorized);
            ContentResult.Content = response.Success
                ? JsonSerializer.SerializeObject(
                    new Shared.Models.Response.LoginResponse(response.AccessToken, response.RefreshToken))
                : JsonSerializer.SerializeObject(response.Errors);
        }
    }
}