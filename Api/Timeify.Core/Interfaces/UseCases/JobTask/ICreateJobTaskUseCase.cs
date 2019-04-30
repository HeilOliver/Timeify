using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;

namespace Timeify.Core.Interfaces.UseCases.JobTask
{
    public interface ICreateJobTaskUseCase : IUseCaseRequestHandler<CreateJobTaskRequest, CreateJobTaskResponse>
    {
    }
}