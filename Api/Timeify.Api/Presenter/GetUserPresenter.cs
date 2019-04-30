using System.Net;
using Timeify.Api.Serialization;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Api.Presenter
{
    [Injectable]
    public class GetUserPresenter : IOutputPort<GetUserResponse>
    {
        public GetUserPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public JsonContentResult ContentResult { get; }

        public void Handle(GetUserResponse response)
        {
            ContentResult.StatusCode = (int) (response.Success ? HttpStatusCode.OK : HttpStatusCode.Gone);
            ContentResult.Content = response.Success
                ? JsonSerializer.SerializeObject(
                    response.User)
                : string.Empty;
        }
    }
}