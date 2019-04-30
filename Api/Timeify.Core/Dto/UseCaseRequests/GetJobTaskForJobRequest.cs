using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseRequests
{
    public class GetJobTaskForJobRequest : IUseCaseRequest<GetJobTaskForJobResponse>
    {
        public GetJobTaskForJobRequest(int jobId, string userId)
        {
            JobId = jobId;
            UserId = userId;
        }

        public int JobId { get; }

        public string UserId { get; }
    }
}