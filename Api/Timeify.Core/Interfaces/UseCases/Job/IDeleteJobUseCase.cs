using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;

namespace Timeify.Core.Interfaces.UseCases.Job
{
    public interface IDeleteJobUseCase : IUseCaseRequestHandler<DeleteJobRequest, DeleteJobResponse>
    {
    }
}