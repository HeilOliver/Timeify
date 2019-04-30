using System.Net;
using Timeify.Api.Serialization;
using Timeify.Common.DI;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Api.Presenter
{
    [Injectable]
    public class GetJobTaskForJobPresenter : IOutputPort<GetJobTaskForJobResponse>
    {
        public GetJobTaskForJobPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public JsonContentResult ContentResult { get; }

        public void Handle(GetJobTaskForJobResponse response)
        {
            ContentResult.StatusCode = (int) (response.Success ? HttpStatusCode.OK : HttpStatusCode.Forbidden);
            ContentResult.Content = response.Success
                ? JsonSerializer.SerializeObject(response.Tasks)
                : JsonSerializer.SerializeObject(response.Errors);
        }
    }
}