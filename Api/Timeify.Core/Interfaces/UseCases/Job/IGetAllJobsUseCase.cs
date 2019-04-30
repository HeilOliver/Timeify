using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;

namespace Timeify.Core.Interfaces.UseCases.Job
{
    public interface IGetAllJobsUseCase : IUseCaseRequestHandler<GetAllJobRequest, GetAllJobResponse>
    {
    }
}