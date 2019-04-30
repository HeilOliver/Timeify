using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;

namespace Timeify.Core.Dto.UseCaseRequests
{
    public class DeleteJobRequest : IUseCaseRequest<DeleteJobResponse>
    {
        public DeleteJobRequest(int jobId, string callerId)
        {
            JobId = jobId;
            CallerId = callerId;
        }

        public int JobId { get; }

        public string CallerId { get; }
    }
}