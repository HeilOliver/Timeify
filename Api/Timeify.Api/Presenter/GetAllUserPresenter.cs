using System.Net;
using Timeify.Api.Serialization;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Api.Presenter
{
    [Injectable]
    public class GetAllUserPresenter : IOutputPort<GetAllUserResponse>
    {
        public GetAllUserPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public JsonContentResult ContentResult { get; }

        public void Handle(GetAllUserResponse response)
        {
            ContentResult.StatusCode =
                (int) (response.Success ? HttpStatusCode.OK : HttpStatusCode.InternalServerError);
            ContentResult.Content = response.Success
                ? JsonSerializer.SerializeObject(
                    response.Users)
                : string.Empty;
        }
    }
}